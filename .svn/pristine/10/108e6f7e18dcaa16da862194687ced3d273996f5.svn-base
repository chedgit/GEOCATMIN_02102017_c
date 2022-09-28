
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports PORTAL_Clases
Imports System.Drawing
Imports ESRI.ArcGIS.esriSystem
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.IO
Imports ESRI.ArcGIS.DataSourcesFile

Public Class Frm_Ingreso_AreasRestringidas


    Private dt As New DataTable
    Public pApp As IApplication
    'Public pEste As Double
    'Public pNorte As Double
    'Private lodtbUbigeo As New DataTable
    'Private cls_Oracle As New cls_Oracle
    'Private cls_Catastro As New cls_DM_1
    'Private cls_Prueba As New cls_Prueba
    'Private cls_DM_2 As New cls_DM_2
    Public m_application As IApplication

    Private Const Col_Sel_R As Integer = 0
    Private Const Col_conta As Integer = 1
    Private Const Col_Codigo As Integer = 2
    Private Const Col_archivo As Integer = 3
    Private Const Col_clase As Integer = 4
    Private Const Col_zona As Integer = 5
    Private Const Col_grafica As Integer = 6

    Private cls_Catastro As New cls_DM_1
    'Private lodtbReporte_Excel As New DataTable
    Private Sub Frm_Ingreso_AreasRestringidas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim fecha As String
        Dim cadena As String
        Dim lodtTabla As New DataTable
        Dim dRow As DataRow
        Dim MyDate As Date
        Dim valor_accion As String = ""
        Dim nm_archivo As String = ""
        Dim nm_clase As String = ""
        Dim nm_zona As String = ""
        Dim codigo As String
        lodtTabla.Clear()
        lodtTabla.Columns.Add("SELEC", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CONTADOR", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("CODIGO", Type.GetType("System.String"))
        lodtTabla.Columns.Add("ARCHIVO", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CLASE", Type.GetType("System.String"))
        lodtTabla.Columns.Add("ZONA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("ACCION", Type.GetType("System.String"))



        'capturando archivo system



        'Cuenta cuantos registros tiene el txtfile
        Dim contando As Integer

        Using archivo1 As IO.StreamReader = New IO.StreamReader("C:\system.txt")
            Dim line As String
            Dim i As Integer
            Do
                line = archivo1.ReadLine
                If line <> "" Then
                    contando = contando + 1

                    If (contando = 1) Then

                        usuario_anm = line
                    ElseIf (contando = 2) Then
                        pasword_anm = line
                    ElseIf (contando = 3) Then
                        coneccion_anm = line
                    ElseIf (contando = 4) Then
                        sesion_anm = line
                    End If
                End If

            Loop Until line Is Nothing

            archivo1.Close()
        End Using
        '   usuario_anm = "CQUI0543"
        '   pasword_anm = "CQUI0545"

        ' usuario_anm = "CEGO0891"
        ' pasword_anm = "CEGO0891"
        'sesion_anm = "999995"


        Dim cls_Conexion As New cls_Oracle
        Dim lostrRetorno1 As New DataTable
        Dim V_USUARIO As String
        Dim v_clave As String

        Try

            lostrRetorno1 = cls_Conexion.F_consultar_usuario(usuario_anm, pasword_anm)

        Catch ex As Exception

        End Try
        'para pruebas

        '  usuario_anm = "CQUI0543"
        '  pasword_anm = "CQUI0544"

        If lostrRetorno1.Rows.Count > 0 Then
            For contador1 As Integer = 0 To lostrRetorno1.Rows.Count - 1
                usuario_anm = lostrRetorno1.Rows(contador1).Item("USUARIO")
                pasword_anm = lostrRetorno1.Rows(contador1).Item("CLAVE")

            Next contador1

        End If



        MyDate = Now

        Dim cuenta As Integer = 0
        Dim cuenta1 As Integer = 0
        'fecha = RellenarComodin(MyDate.Day, 2, "0") & RellenarComodin(MyDate.Month, 2, "0") & RellenarComodin(MyDate.Year, 2, "0")
        fecha = RellenarComodin(MyDate.Day, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & RellenarComodin(MyDate.Year, 2, "0")
        '   fecha = "20/02/2015"

        'fecha = "11/06/2015"
        '   fecha = "27/11/2015"

        Dim contador_lineas As Long


        ' ''Cuenta cuantos registros tiene el txtfile

        ''Using archivo1 As IO.StreamReader = New IO.StreamReader("C:\" & fecha & ".txt")
        ''    Dim line As String
        ''    Dim i As Integer
        ''    Do
        ''        line = archivo1.ReadLine
        ''        If line <> "" Then
        ''            cuenta = cuenta + 1
        ''            cadena = line
        ''            If (cadena = "ANDG") Then
        ''            valor_accion = "INGRESO"
        ''            v_tipoproceso = "INGRESAR"
        ''            ElseIf (cadena = "ANDM") Then
        ''            valor_accion = "MODIFICACION"
        ''            v_tipoproceso = "MODIFICAR"
        ''            ElseIf (cadena = "ANDE") Then
        ''                valor_accion = "ELIMINACION"
        ''            v_tipoproceso = "ELIMINAR"
        ''            Else
        ''                colecciones_txtfiles.Add(cadena)  'Solo apartir de la 2da linea del txt
        ''            End If
        ''        End If

        ''    Loop Until line Is Nothing

        ''    archivo1.Close()
        ''End Using

        ' ''Validando 

        ''Dim valor_txt_txtfile As Long
        ''Dim valor_ind_txtfile As Integer
        ''Dim valor_txt As String = ""
        ''Dim cuenta2 As Integer = 0
        ''Dim valor_cuenta2 As Integer = 0
        ''Dim valor_cuenta3 As Integer = 0
        ''Dim valor_cuenta4 As Integer = 0
        ''Dim cuenta_f As Long = 0
        ''valor_txt_txtfile = 0
        ''valor_txt_txtfile = colecciones_txtfiles.Count
        ''valor_ind_txtfile = valor_txt_txtfile / 4  'haciendo multiple de 4 para capturar los elementos

        ''For contador1 As Integer = 1 To valor_ind_txtfile  'hace boocle de 4 en 4
        ''    dRow = lodtTabla.NewRow

        ''    cuenta_f = 4 * contador1 - 4
        ''    valor_cuenta4 = cuenta_f + 4
        ''    cuenta2 = cuenta2 + 1

        ''    If contador1 = cuenta2 Then

        ''        For contador2 As Integer = cuenta_f + 1 To valor_cuenta4 'construyendo la contante para tomar de 4 en 4
        ''            valor_txt = ""
        ''            valor_cuenta3 = valor_cuenta3 + 1
        ''            valor_txt = colecciones_txtfiles.Item(contador2)
        ''            'Almacena al datatable
        ''            If valor_cuenta3 = 1 Then
        ''                dRow.Item("CODIGO") = valor_txt
        ''            ElseIf valor_cuenta3 = 2 Then
        ''                dRow.Item("ARCHIVO") = Microsoft.VisualBasic.Right(valor_txt, Len(valor_txt) - 2)
        ''            ElseIf valor_cuenta3 = 3 Then
        ''                dRow.Item("CLASE") = Microsoft.VisualBasic.Left(valor_txt, 1)
        ''            ElseIf valor_cuenta3 = 4 Then
        ''                dRow.Item("ZONA") = valor_txt

        ''            End If
        ''            dRow.Item("CONTADOR") = cuenta2
        ''            dRow.Item("ACCION") = valor_accion
        ''        Next contador2
        ''        valor_cuenta3 = 0  'haciendo 0 para ir de 4 en 4 en cero
        ''        lodtTabla.Rows.Add(dRow)
        ''    End If

        'Next contador1

        'cambiando la forma de traer datos a procesar de areas restringidas
        Dim nomb_archivo As String
        Dim tp_archivo As String
        Dim zona_archivo As String
        Dim fec_archivo As String
        Dim cuenta2 As Integer = 0
        Dim valor_txt As String = ""
        '   Dim v_fec_libden_sel As String
        fecha = Microsoft.VisualBasic.Left(fecha, 10)
        Try

            lostrRetorno1 = cls_Conexion.FT_consultar_proceso_Anm("001", fecha)

        Catch ex As Exception

        End Try


        If lostrRetorno1.Rows.Count > 0 Then
            For contador1 As Integer = 0 To lostrRetorno1.Rows.Count - 1
                dRow = lodtTabla.NewRow
                cuenta2 = cuenta2 + 1
                cadena = lostrRetorno1.Rows(contador1).Item("RE_MODREG")

                If (cadena = "ANDG") Then
                    valor_accion = "INGRESO"
                    v_tipoproceso = "INGRESAR"

                ElseIf (cadena = "ANDM") Then
                    valor_accion = "MODIFICACION"
                    v_tipoproceso = "MODIFICAR"
                ElseIf (cadena = "ANDE") Then
                    valor_accion = "ELIMINACION"
                    v_tipoproceso = "ELIMINAR"
                End If


                valor_txt = lostrRetorno1.Rows(contador1).Item("CG_CODIGO")
                nomb_archivo = lostrRetorno1.Rows(contador1).Item("RE_ARCGRA")
                tp_archivo = lostrRetorno1.Rows(contador1).Item("RE_TIPGRA")
                zona_archivo = lostrRetorno1.Rows(contador1).Item("RE_NUMZON")
                fec_archivo = lostrRetorno1.Rows(contador1).Item("RE_FECREG")

                dRow.Item("CODIGO") = valor_txt
                dRow.Item("ARCHIVO") = nomb_archivo
                dRow.Item("CLASE") = Microsoft.VisualBasic.Left(tp_archivo, 1)
                dRow.Item("ZONA") = zona_archivo
                dRow.Item("ACCION") = valor_accion
                dRow.Item("CONTADOR") = cuenta2
                lodtTabla.Rows.Add(dRow)
            Next contador1

        End If



        Me.dgdDetalle.DataSource = lodtTabla

        PT_Estilo_Grilla_AreasRestringidas(lodtTabla) : PT_Cargar_Grilla_AreasRestringidas(lodtTabla)
        PT_Agregar_Funciones_AreasRestringidas() : PT_Forma_Grilla_Funciones_AreasRestringidasL()


        Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
        For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
            dgdDetalle.Item(i, "SELEC") = False
        Next
        Me.dgdDetalle.AllowUpdate = True
        dRow = Nothing
        dgdDetalle.Focus()
        colecciones_txtfiles.Clear()

    End Sub
    Private Sub PT_Estilo_Grilla_AreasRestringidas(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Sel_R).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_archivo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_clase).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_zona).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_grafica).DefaultValue = ""

    End Sub
    Private Sub PT_Cargar_Grilla_AreasRestringidas(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Me.dgdDetalle.Columns(Col_Sel_R).Value = True
    End Sub

    Private Sub PT_Agregar_Funciones_AreasRestringidas()
        Me.dgdDetalle.Columns(Col_Sel_R).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_archivo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_clase).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_zona).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_grafica).DefaultValue = ""


    End Sub
    Private Sub chkEstado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEstado.CheckedChanged


        Dim items As C1.Win.C1TrueDBGrid.ValueItems = Me.dgdDetalle.Columns("SELEC").ValueItems
        If Me.chkEstado.Checked Then
            items.Translate = True
            items.CycleOnClick = True
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", False)) ' unchecked
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", True)) ' checked

        Else
            items.Translate = True
            items.CycleOnClick = True
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", False)) ' unchecked
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", True)) ' checked
        End If
    End Sub



    Private Sub PT_Forma_Grilla_Funciones_AreasRestringidasL()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Width = 70
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_archivo).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_clase).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grafica).Width = 100

        Me.dgdDetalle.Columns("SELEC").Caption = "SEL."
        Me.dgdDetalle.Columns("CONTADOR").Caption = "CONTADOR"
        Me.dgdDetalle.Columns("CODIGO").Caption = "CODIGO"
        Me.dgdDetalle.Columns("ARCHIVO").Caption = "ARCHIVO"
        Me.dgdDetalle.Columns("CLASE").Caption = "CLASE"
        Me.dgdDetalle.Columns("ZONA").Caption = "ZONA"
        Me.dgdDetalle.Columns("ACCION").Caption = "ACCION"

        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Blue
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_archivo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_clase).Locked = True

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grafica).Locked = True

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_archivo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_clase).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grafica).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_archivo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_clase).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grafica).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center


    End Sub

    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click



        cls_Catastro.Borra_Todo_Feature("", m_application)
        pMxDoc.UpdateContents()

        'Me.Close()
        Try
            Try
                Kill(glo_pathTMP & "\geowgs84.shp")
                Kill(glo_pathTMP & "\geowgs84.dbf")
                Kill(glo_pathTMP & "\geowgs84.shx")
                Kill(glo_pathTMP & "\geowgs84.prj")
                Kill(glo_pathTMP & "\geowgs84.sbn")
                Kill(glo_pathTMP & "\geowgs84.sbx")

                Kill(glo_pathTMP & "\geopasad56.shp")
                Kill(glo_pathTMP & "\geopasad56.dbf")
                Kill(glo_pathTMP & "\geopasad56.shx")
                Kill(glo_pathTMP & "\geopasad56.prj")
                Kill(glo_pathTMP & "\geopasad56.sbn")
                Kill(glo_pathTMP & "\geopasad56.sbx")
                Kill(glo_pathTMP & "\geopasad56.sr.lock")
            Catch ex As Exception

            End Try

            '   usuario_anm = "CQUI0543"
            '  pasword_anm = "CQUI0544"
            v_sistema = "WGS-84"
            Dim cls_Oracle As New cls_Oracle
            Dim v_cod_rese As String = ""
            '   Dim v_archivo As String = """
            Dim v_zona_rese As String = ""
            Dim v_clase_rese As String = ""
            Dim v_acccion As String = ""
            Dim v_clase_sele As String = ""
            Dim pFeatureCursor As IFeatureCursor
            Dim pfeature As IFeature
            Dim pQueryFilter As IQueryFilter

            Dim pFeatureTable As ITable
            Dim val_nombre As String = ""
            Dim val_descrinorma As String = ""

            Dim val_fecpub As String = ""
            ' Dim val_fecpub1 As String = ""
            Dim val_nm_rese As String = ""
            Dim val_codtip As String = ""
            Dim val_destip As String = ""
            Dim val_nucleo As String = ""
            Dim val_cate As String = ""
            Dim val_nucleo1 As String = ""
            Dim val_codcat As String = ""
            Dim val_norma As String = ""
            Dim val_zonaexp As String = ""
            Dim val_fecing As String = ""
            Dim val_entidad As String = ""
            Dim val_uso As String = ""
            Dim val_estgraf As String = ""
            Dim val_estgraf1 As String = ""
            Dim LEYENDA As String = ""
            Dim canti_reg As Integer

            Dim V_area_anm As Double
            Dim V_area_anm1 As Double
            Dim V_area_anm2 As Double
            Dim V_area_anm_PSAD As Double
            Dim V_area_anm_g56 As Double
            Dim V_area_anm_g84 As Double
            Dim pFeatureCursorpout As IFeatureCursor
            Dim pFeaturepout As IFeature
            Dim pFeaturepout1 As IFeature

            'Me.dgdDetalle.Columns("SELEC").Caption = "SELEC"
            'Me.dgdDetalle.Columns("CONTADOR").Caption = "CONTADOR"
            'Me.dgdDetalle.Columns("CODIGO").Caption = "CODIGO"
            'Me.dgdDetalle.Columns("ARCHIVO").Caption = "ARCHIVO"
            'Me.dgdDetalle.Columns("CLASE").Caption = "CLASE"
            'Me.dgdDetalle.Columns("ZONA").Caption = "ZONA"
            'Me.dgdDetalle.Columns("ACCION").Caption = "ACCION"
            Dim lodbtExiste_rese As DataTable

            Dim loBoo_flg As Boolean = False

            Dim cls_catastro As New cls_DM_1
            Dim cls_eval As New Cls_evaluacion

            ' Dim v_tipoproceso As String = "INGRESAR"
            '  tipo_catanominero = "AREA RESERVADA"
            Dim v_zona_sele As String
            Dim val_fecing1 As Date
            Dim val_fecing2 As String = "yyyy-mm-dd"
            '  Dim val_fecpub1 As Date
            Dim val_fecpub1 As String = ""
            Dim intArea As Integer
            Dim pArea As IArea
            Dim v_zona_inicio As String
            Dim sele_tipo As String


            '  Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
            V_area_anm1 = 0
            V_area_anm_g84 = 0
            V_area_anm_g56 = 0
            For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
                ' For w As Integer = 0 To Me.dgdDetalle.RowCount - 1

                ' For w As Integer = 0 To Me.dgdDetalle.CheckForIllegalCrossThreadCalls - 1

                'If Me.dgdDetalle.Item(w, "SELEC") Then  'valida lo seleccionado
                If dgdDetalle.Item(w, "SELEC") = True Then
                    ' v_tipoproceso = "INGRESAR"
                    loBoo_flg = True


                    ' For proceso As Integer = 1 To 2


                    '  If proceso = 1 Then   'PARA PROCESO WGS 84
                    'v_sistema = "WGS_84"

                    ' ElseIf proceso = 2 Then  'PARA PROCESO EN PSAD 56 (NO ANAPS)

                    '   v_sistema = "PSAD-56"
                    ' End If

                    cls_catastro.Borra_Todo_Feature("", m_application)
                    v_cod_rese = dgdDetalle.Item(w, "CODIGO").ToString

                    '   tipo_catanominero = Microsoft.VisualBasic.Left(v_cod_rese, 2)
                    sele_tipo = Microsoft.VisualBasic.Left(v_cod_rese, 2)
                    If sele_tipo = "ZU" Then

                        tipo_catanominero = "ZONA URBANA"
                        nombre_carpeta = "URBANAS"
                    Else
                        tipo_catanominero = "AREA RESERVADA"
                        nombre_carpeta = "RESERVAS"
                    End If
                    v_archivo = dgdDetalle.Item(w, "ARCHIVO").ToString.ToUpper
                    v_clase_rese = dgdDetalle.Item(w, "CLASE").ToString
                    v_zona_rese = dgdDetalle.Item(w, "ZONA").ToString
                    v_zona_inicio = v_zona_rese
                    v_acccion = dgdDetalle.Item(w, "ACCION").ToString

                    If v_acccion = "INGRESO" Then
                        v_tipoproceso = "INGRESAR"
                    ElseIf v_acccion = "MODIFICACION" Then
                        v_tipoproceso = "MODIFICAR"
                    ElseIf v_acccion = "ELIMINACION" Then
                        v_tipoproceso = "ELIMINAR"
                    End If

                    If v_zona_rese = "17" Then
                        rese_17_Shapefile = "Area_" & DateTime.Now.Ticks.ToString
                        v_archivo_rese = rese_17_Shapefile
                    ElseIf v_zona_rese = "18" Then
                        rese_18_Shapefile = "Area_" & DateTime.Now.Ticks.ToString
                        v_archivo_rese = rese_18_Shapefile
                    ElseIf v_zona_rese = "19" Then
                        rese_19_Shapefile = "Area_" & DateTime.Now.Ticks.ToString
                        v_archivo_rese = rese_19_Shapefile
                    End If
                    m_application.Caption = "Procesando Area Restringida:  " & v_cod_rese & "   con Archivo : " & v_archivo.ToString & " en Zona :" & v_zona_rese
                    Try
                        lodbtExiste_rese = cls_Oracle.FT_Ver_Area_Restringida(v_cod_rese, v_clase_rese)

                    Catch ex As Exception
                        ex.Message.ToString()
                    End Try

                    If lodbtExiste_rese.Rows.Count >= 1 Then

                        ' MsgBox(lodbtExiste_rese.Rows.Count, "X")

                        For contador As Integer = 0 To lodbtExiste_rese.Rows.Count - 1
                            v_cod_rese = lodbtExiste_rese.Rows(contador).Item("CG_CODIGO").ToString
                            v_clase_rese = lodbtExiste_rese.Rows(contador).Item("PA_CODPAR").ToString
                            v_zona_rese = lodbtExiste_rese.Rows(contador).Item("ZA_ZONA").ToString
                            'If v_zona_rese = "17" Then
                            'v_archivo = lodbtExiste_rese.Rows(contador).Item("AN_ARCZ17").ToString

                            'ElseIf v_zona_rese = "18" Then
                            'v_archivo = lodbtExiste_rese.Rows(contador).Item("AN_ARCZ18").ToString
                            'ElseIf v_zona_rese = "19" Then
                            'v_archivo = lodbtExiste_rese.Rows(contador).Item("AN_ARCZ19").ToString
                            'End If

                            val_nombre = lodbtExiste_rese.Rows(contador).Item("PE_NOMARE").ToString
                            val_codtip = lodbtExiste_rese.Rows(contador).Item("Tn_codtip").ToString
                            val_destip = lodbtExiste_rese.Rows(contador).Item("tn_destip").ToString

                            'Se aumento 2 campos mas para buscar del procedimiento..

                            val_descrinorma = lodbtExiste_rese.Rows(contador).Item("NL_DESNOR").ToString

                            '  val_fecpub = lodbtExiste_rese.Rows(contador).Item("AN_PUBNOR").ToString
                            Try
                                val_fecpub1 = lodbtExiste_rese.Rows(contador).Item("AN_PUBNOR")

                            Catch ex As Exception
                            End Try

                            '   If IsDate(val_fecpub1) = True Then

                            'val_fecpub = val_fecpub1.ToString
                            val_fecpub = val_fecpub1  'mismo formato fecha



                            ' val_nucleo = lodbtExiste_rese.Rows(contador).Item("Pa_codpar")
                            val_cate = lodbtExiste_rese.Rows(contador).Item("CT_DESCRI").ToString
                            If (v_clase_rese = "N") Then
                                If ((val_cate = "PARQUE NACIONAL") Or (val_cate = "SANTUARIO NACIONAL") Or (val_cate = "SANTUARIO HISTORICO") Or (val_cate = "RESERVA PAISAJISTICA") Or (val_cate = "REFUGIO DE VIDA SILVESTRE") Or (val_cate = "RESERVA NACIONAL") Or (val_cate = "RESERVA COMUNAL") Or (val_cate = "COTO DE CAZA") Or (val_cate = "BOSQUE DE PROTECCION") Or (val_cate = "ZONA RESERVADA")) Then

                                    val_nucleo1 = "ANP"
                                Else
                                    'val_nucleo1 = "   "
                                    val_nucleo1 = " "
                                End If

                            ElseIf (v_clase_rese = "A") Then
                                val_nucleo1 = "AMORTIGUAMIENTO"
                            End If

                            val_codcat = lodbtExiste_rese.Rows(contador).Item("CA_CODCAT").ToString
                            val_norma = lodbtExiste_rese.Rows(contador).Item("AN_NORLEG").ToString
                            If tipo_catanominero = "AREA RESERVADA" Then

                            Else  'ZONA URBANA
                                If (val_norma = "") Then
                                    val_norma = "NO"
                                Else
                                    val_norma = "SI"
                                End If
                            End If

                            val_zonaexp = lodbtExiste_rese.Rows(contador).Item("ZA_ZONA").ToString
                            ' val_fecing1 = lodbtExiste_rese.Rows(contador).Item("AN_FECING")
                            val_fecing1 = lodbtExiste_rese.Rows(contador).Item("AN_FECING")
                            val_entidad = lodbtExiste_rese.Rows(contador).Item("AN_ENTIDAD").ToString
                            If IsDate(val_fecing1) = True Then
                                val_fecing = val_fecing1.ToString
                                'Dim todaysdate As String = String.Format("{dd/MM/yyyy}", DateTime.Now)
                                Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
                                'Dim todaysdate As String = String.Format("{0:yyyy/MM/dd}", DateTime.Now)
                                'val_fecing = val_fecing1.ToString(val_fecing2)
                                val_fecing = val_fecing1.ToString(todaysdate)
                                ' MsgBox(val_fecing)
                            Else
                                '  v_fec_denun_x = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
                            End If
                            '  val_fecing1 = val_fecing
                            '    val_fecing = val_fecing1.ToString(val_fecing1)



                            If (val_destip = "AREA NATURAL") Then
                                If ((val_cate = "RESERVA PAISAJISTICA") Or (val_cate = "REFUGIO DE VIDA SILVESTRE") Or (val_cate = "RESERVA COMUNAL") Or (val_cate = "BOSQUE DE PROTECCION") Or (val_cate = "RESERVA NACIONAL") Or (val_cate = "COTO DE CAZA")) Then
                                    val_uso = "USO DIRECTO"
                                ElseIf ((val_cate = "PARQUE NACIONAL") Or (val_cate = "SANTUARIO NACIONAL") Or (val_cate = "SANTUARIO HISTORICO")) Then
                                    val_uso = "USO INDIRECTO"
                                Else
                                    val_uso = " "
                                End If
                            End If

                            val_estgraf = lodbtExiste_rese.Rows(contador).Item("AN_ESTGRA").ToString

                            If (val_estgraf = "01") Then
                                val_estgraf1 = "DEFINTIVO"
                                LEYENDA = "DEFINITIVO"
                            ElseIf (val_estgraf = "02") Then
                                val_estgraf1 = "REFERENCIAL"
                                LEYENDA = "REFERENCIAL"
                            ElseIf (val_estgraf = "03") Then
                                val_estgraf1 = "NO GRAFICA"
                                LEYENDA = " "
                            ElseIf (val_estgraf = "04") Then
                                val_estgraf1 = "PROVISIONAL"
                                LEYENDA = "REFERENCIAL"
                            Else
                                val_estgraf1 = " "
                            End If

                            'If (val_estgraf = "01") Then
                            '    LEYENDA = "DEFINITIVO"
                            'ElseIf (val_estgraf = "02") Then
                            '    LEYENDA = "REFERENCIAL"
                            'ElseIf (val_estgraf = "04") Then
                            '    LEYENDA = "REFERENCIAL"
                            'End If
                            ' v_zona_rese = "17"

                            '  cls_catastro.Actualizar_DM_psad56(v_zona_rese)
                            'Next contador

                            ' End If
                            'cls_Eval.consultacapaDM("", "LibreDen", "Catastro")

                            'cls_catastro.Exportando_Temas("", "Catastro", pApp)
                            'cls_catastro.Quitar_Layer("Catastro", pApp)
                            ' cls_catastro.AddFeature_rese(v_archivo, m_application)

                            cls_catastro.AddFeature_rese(v_archivo, m_application)
                            '   Exit Sub
                            cls_eval.agregacampotema_tpm(v_archivo, "CODIGO")
                            'cls_catastro.Quitar_Layer("Catastro", pApp)
                            cls_eval.consultacapaDM("", "AREAS_RESE", v_archivo)
                            arch_cata = "Rese_exp"
                            cls_catastro.Exportando_Temas("", v_archivo, pApp)
                            cls_catastro.Quitar_Layer(v_archivo, pApp)
                            '  cls_catastro.Add_ShapeFile_tmp(v_archivo_rese, pApp)
                            'se comento esta parte 1
                            cls_catastro.Add_ShapeFile_tmp_reseurba(v_archivo_rese, pApp)
                            '   Exit Sub

                            ''cls_catastro.AddFeature_rese(v_archivo_rese, m_application)
                            '' Exit Sub

                            cls_eval.agregacampotema_tpm(v_archivo_rese, tipo_catanominero)
                            ''Exit Sub

                            'Dim canti_reg As Integer

                            ''cls_catastro.UpdateValue("", pApp, v_archivo_rese)

                            If pFeatureLayer_tmp.FeatureClass.FeatureCount(Nothing) = 0 Then
                                MsgBox("No hay registros en la capa ", MsgBoxStyle.Critical, "Observación...")
                                Exit Sub
                            Else
                                canti_reg = pFeatureLayer_tmp.FeatureClass.FeatureCount(Nothing)
                            End If
                            '  MsgBox(canti_reg)
                            pQueryFilter = New QueryFilter

                            ' pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)

                            pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)
                            'AQUI NUEVO
                            ' pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)

                            pFeaturepout1 = pFeatureCursor.NextFeature

                            Do While Not pFeaturepout1 Is Nothing
                                'Do While pFeaturepout1 Is Nothing

                                If v_zona_rese = v_zona_inicio Then
                                    pArea = pFeaturepout1.Shape
                                    V_area_anm = pArea.Area / 10000
                                End If

                                If tipo_catanominero = "AREA RESERVADA" Then

                                    pFeaturepout1.Value(pFeatureCursor.FindField("NOMBRE")) = val_nombre
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("NM_RESE")) = val_nombre & " - " & val_destip
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("CODIGO")) = v_cod_rese
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("TP_RESE")) = val_destip
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("CATEGORI")) = val_cate
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("CLASE")) = val_nucleo1 ' v_clase_rese
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("ZONA")) = v_zona_rese
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("ZONEX")) = val_zonaexp
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    'pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
                                    'pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                    ' pFeaturepout1.Value(pFeatureCursor.FindField("NORMA")) = val_norma


                                    If v_clase_rese = "N" Then
                                        pFeaturepout1.Value(pFeatureCursor.FindField("NORMA")) = val_norma & "-" & val_fecpub
                                    Else
                                        pFeaturepout1.Value(pFeatureCursor.FindField("NORMA")) = val_descrinorma & "-" & val_fecpub
                                    End If
                                    ' pFeaturepout1.Value(pFeatureCursor.FindField("NORMA")) = val_descrinorma & "-" & val_norma

                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("ARCHIVO")) = v_archivo
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)

                                    pFeaturepout1.Value(pFeatureCursor.FindField("FEC_ING")) = val_fecing.ToString
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)

                                    ' pFeaturepout1.Value(pFeatureCursor.FindField("FEC_PUB")) = val_fecpub.ToString
                                    If IsDate(val_fecpub) = True Then

                                        pFeaturepout1.Value(pFeatureCursor.FindField("FEC_PUB")) = val_fecpub
                                    End If

                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("ENTIDAD")) = val_entidad
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("USO")) = val_uso
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)

                                    pFeaturepout1.Value(pFeatureCursor.FindField("EST_GRAF")) = val_estgraf1
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("LEYENDA")) = LEYENDA
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("HAS")) = V_area_anm

                                    'If v_sistema = "PSAD-56" Then

                                    '    pFeaturepout1.Value(pFeatureCursor.FindField("HAS")) = V_area_anm_PSAD

                                    'Else
                                    '    pFeaturepout1.Value(pFeatureCursor.FindField("HAS")) = V_area_anm2
                                    'End If

                                    pFeatureCursor.UpdateFeature(pFeaturepout1)

                                ElseIf tipo_catanominero = "ZONA URBANA" Then

                                    pFeaturepout1.Value(pFeatureCursor.FindField("NM_URBA")) = val_nombre & " - " & val_destip
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("CODIGO")) = v_cod_rese
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("NOMBRE")) = val_nombre
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    '   pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = v_area
                                    '   pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("TP_URBA")) = val_destip
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("CATEGORI")) = val_cate
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("ORDENANZA")) = val_norma
                                    'pFeaturepout1.Value(pFeatureCursor.FindField("ORDENANZA")) = val_descrinorma & "-" & val_norma

                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("ZONA")) = v_zona_rese
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("ZONEX")) = val_zonaexp
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)

                                    ' pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
                                    ' pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                    'pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = v_norma
                                    'pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("ARCHIVO")) = v_archivo
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("FEC_ING")) = val_fecing.ToString
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    If IsDate(val_fecpub) = True Then

                                        pFeaturepout1.Value(pFeatureCursor.FindField("FEC_PUB")) = val_fecpub.ToString
                                    End If

                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("ENTIDAD")) = val_entidad
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("USO")) = val_uso
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("EST_GRAF")) = val_estgraf1
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)
                                    pFeaturepout1.Value(pFeatureCursor.FindField("LEYENDA")) = LEYENDA
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)

                                    pFeaturepout1.Value(pFeatureCursor.FindField("HAS")) = V_area_anm
                                    pFeatureCursor.UpdateFeature(pFeaturepout1)

                                    '  pFeaturepout = pFeatureCursorpout.NextFeature
                                End If

                                '     End If

                                pFeaturepout1 = pFeatureCursor.NextFeature
                                ' pFeaturepout = pFeatureCursorpout.NextFeature
                            Loop


                            '***********************se comenta 14-09-16
                            '  pfeature = pFeatureCursor.NextFeature

                            arch_cata = v_archivo_rese
                            cls_catastro.Color_Poligono_Simple(m_application, v_archivo_rese)
                            ' cls_catastro.Zoom_to_Layer(v_archivo_rese)
                            ' cls_catastro.rotulatexto_dm(v_archivo_rese, pApp)
                            cls_catastro.ShowLabel_DM(v_archivo_rese, m_application)

                            '  Exit Sub

                            'Genera la imagen previa y consultara si existe para guardar en base de datos

                            Dim v_imagen As String = ""

                            'HOY SE CAMBIO RUTA IMAGEN 26-09-16
                            ' -----------------
                            Dim lostrRetorno As String

                            If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & ".bmp") Then

                                If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v2.bmp") Then

                                    If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v3.bmp") Then

                                        If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v4.bmp") Then

                                            If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v5.bmp") Then

                                                If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v6.bmp") Then


                                                Else
                                                    v_imagen = v_cod_rese & "v6"
                                                    cls_catastro.Genera_Imagen_DM(v_cod_rese & "v6", "ANM")
                                                    lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v6" & ".bmp", usuario_anm, v_sistema)
                                                End If
                                            Else
                                                v_imagen = v_cod_rese & "v5"
                                                cls_catastro.Genera_Imagen_DM(v_cod_rese & "v5", "ANM")
                                                lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v5" & ".bmp", usuario_anm, v_sistema)
                                            End If
                                        Else
                                            v_imagen = v_cod_rese & "v4"
                                            cls_catastro.Genera_Imagen_DM(v_cod_rese & "v4", "ANM")
                                            lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v4" & ".bmp", usuario_anm, v_sistema)
                                        End If
                                    Else
                                        v_imagen = v_cod_rese & "v3"
                                        cls_catastro.Genera_Imagen_DM(v_cod_rese & "v3", "ANM")
                                        lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v3" & ".bmp", usuario_anm, v_sistema)
                                    End If

                                Else
                                    v_imagen = v_cod_rese & "v2"
                                    cls_catastro.Genera_Imagen_DM(v_cod_rese & "v2", "ANM")
                                    lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v2" & ".bmp", usuario_anm, v_sistema)
                                End If
                            Else
                                v_imagen = v_cod_rese
                                '  cls_catastro.Genera_Imagen_DM(v_cod_rese & ".bmp", "ANM")
                                cls_catastro.Genera_Imagen_DM(v_cod_rese, "ANM")
                                lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & ".bmp", usuario_anm, v_sistema)

                            End If

                            cls_catastro.Limpiar_Texto_Pantalla(m_application)

                            'AQUI TERMINO

                            pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)
                            pfeature = pFeatureCursor.NextFeature
                            'se comento final  parte 1
                            ' cls_catastro.Actualizar_DM_psad56(v_zona_rese)
                            cls_catastro.Actualizar_DM(v_zona_rese)

                            If v_zona_rese = "17" Then
                                For i As Integer = 1 To 3
                                    If i = "1" Then
                                        cls_catastro.proyectacapas("18", v_zona_rese, v_sistema)
                                    ElseIf i = "2" Then
                                        cls_catastro.proyectacapas("19", v_zona_rese, v_sistema)
                                    ElseIf i = "3" Then
                                        cls_catastro.proyectacapas("GEO", v_zona_rese, v_sistema)
                                        'ElseIf i = "4" Then
                                        '    cls_catastro.proyectacapas("GEOPSAD", v_zona_rese, v_sistema)
                                    End If
                                Next i
                            ElseIf v_zona_rese = "18" Then
                                For i As Integer = 1 To 3
                                    If i = "1" Then
                                        cls_catastro.proyectacapas("17", v_zona_rese, v_sistema)
                                    ElseIf i = "2" Then
                                        cls_catastro.proyectacapas("19", v_zona_rese, v_sistema)
                                    ElseIf i = "3" Then
                                        cls_catastro.proyectacapas("GEO", v_zona_rese, v_sistema)
                                        'ElseIf i = "4" Then
                                        '    cls_catastro.proyectacapas("GEOPSAD", v_zona_rese, v_sistema)
                                    End If
                                Next i
                            ElseIf v_zona_rese = "19" Then
                                For i As Integer = 1 To 3
                                    If i = "1" Then
                                        cls_catastro.proyectacapas("17", v_zona_rese, v_sistema)
                                    ElseIf i = "2" Then
                                        cls_catastro.proyectacapas("18", v_zona_rese, v_sistema)
                                    ElseIf i = "3" Then
                                        cls_catastro.proyectacapas("GEO", v_zona_rese, v_sistema)
                                        'ElseIf i = "4" Then
                                        '    cls_catastro.proyectacapas("GEOPSAD", v_zona_rese, v_sistema)
                                    End If
                                Next i
                            End If

                            If nombre_geowgs_psad = "SI" Then

                                'Artificio para proyectar area reservas wgs 84 geodesico a psad 56 geodesico
                                '----------------------------------------------------------------------------

                                Dim RetVal

                                While RetVal Is Nothing
                                    glo_Path_EXE = "U:\DATOS\DBF\"
                                    'glo_Path_EXE1 = "e:\proyecta\"
                                    RetVal = Shell(glo_Path_EXE & "proywgs_psad1.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea

                                    ' Antes ejecutado en net no sale bien la proyeccion en geodesico
                                    'cls_catastro.proyectacapas_geopasad("GEOPSAD", v_zona_rese, v_sistema)
                                    '
                                End While

                                'Se puso para evitar bloque de capa geodesico

                                rese_geo_Shapefile_WGEO_PSAD = "geopasad56"
                                cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile_WGEO_PSAD, pApp)

                                cls_eval.consultacapaDM("", "AREAS_RESE", rese_geo_Shapefile_WGEO_PSAD)

                                arch_cata = "geowgwspsad"
                                cls_catastro.Exportando_Temas("", rese_geo_Shapefile_WGEO_PSAD, pApp)

                                'sigue normal

                                For i As Integer = 17 To 19
                                    If i = "17" Then

                                        cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)

                                    ElseIf i = "18" Then

                                        cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)

                                    ElseIf i = "19" Then

                                        cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)
                                    End If
                                Next i

                            End If

                            Dim validar As String = ""

                            Dim z1 As String

                            ' For z As Integer = 17 To 20  'comentado el 24-10-16      '  **********************************************************
                            For z As Integer = 17 To 24
                                '  For z As Integer = 20 To 20
                                '  If (z = "21") Or (z = "22") Or (z = "23") Or (z = "24") Then
                                If z = "21" Then       ' ------------------------------------------------------    CAMBIO 21 A 20 PARA PROBAR
                                    v_sistema = "PSAD-56"
                                    z1 = "17"
                                    'z = "21"         ' ------------------  se agrego para probar
                                    validar = "No"
                                ElseIf z = "22" Then
                                    v_sistema = "PSAD-56"
                                    z1 = "18"
                                    validar = "No"
                                ElseIf z = "23" Then
                                    z1 = "19"
                                    v_sistema = "PSAD-56"
                                    validar = "No"
                                End If

                                If v_zona_rese = z Then
                                    validar = "No"
                                    pMap = pMxDoc.FocusMap
                                    pFeatureLayer = pMap.Layer(0)
                                    '    MsgBox(pFeatureLayer.Name)
                                    ' cls_catastro.Borra_Todo_Feature("", pApp)

                                    pMap.DeleteLayer(pFeatureLayer)
                                    pMxDoc.UpdateContents()
                                    If tipo_catanominero = "AREA RESERVADA" Then

                                        ' glo_User_SDE = "CEGO0891"

                                        If v_sistema = "PSAD-56" Then
                                            '  cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1 & "D", m_application, "1", False)
                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
                                            'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
                                        Else
                                            ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & z & "W", m_application, "1", False)
                                            'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z & "D", m_application, "1", False)

                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z, m_application, "1", False)

                                        End If

                                        'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_18", m_application, "1", False)
                                    ElseIf tipo_catanominero = "ZONA URBANA" Then
                                        If v_sistema = "PSAD-56" Then
                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_" & z1, m_application, "1", False)
                                        Else

                                            'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)
                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)
                                        End If
                                    End If
                                    'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z, m_application, "1", False)
                                    v_zona_sele = v_zona_rese

                                ElseIf (z = "20") Or (z = "24") Then

                                    If z = "20" Then
                                        validar = "SI geo"
                                    Else
                                        validar = "No"
                                    End If
                                    If v_sistema = "PSAD-56" Then
                                        cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile_WGEO_PSAD, pApp)

                                    Else
                                        cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile, pApp)
                                    End If

                                    '  cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile, pApp)
                                    pQueryFilter = New QueryFilter
                                    pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)
                                    pfeature = pFeatureCursor.NextFeature


                                    pMap = pMxDoc.FocusMap
                                    pFeatureLayer = pMap.Layer(0)
                                    '  cls_catastro.Borra_Todo_Feature("", pApp)

                                    pMap.DeleteLayer(pFeatureLayer)
                                    pMxDoc.UpdateContents()
                                    If tipo_catanominero = "AREA RESERVADA" Then
                                        'cls_catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_G56", m_application, "1", False)
                                        If v_sistema = "PSAD-56" Then

                                            'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G56" & "D", m_application, "1", False)
                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G56", m_application, "1", False)
                                            'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_G56P", m_application, "1", False)
                                        Else
                                            'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_G84", m_application, "1", False)
                                            ' cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G84" & "D", m_application, "1", False) 'SE DESCONEMTO 10-03-17
                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G84", m_application, "1", False)  'SE COMENTO HOY 10-03-17
                                            ' cls_catastro.PT_CargarFeatureClass_SDE("DESA_GIS.GPO_ARE_AREA_RESERVADA_G", m_application, "1", False)  'SE COMENTO HOY 10-03-17
                                            '  cls_catastro.PT_CargarFeatureClass_SDE("DESA_GIS.GPO_ARE_AREA_RESERVADA_G84D", m_application, "1", False)  'SE COMENTO HOY 10-03-17
                                        End If

                                    ElseIf tipo_catanominero = "ZONA URBANA" Then
                                        If v_sistema = "PSAD-56" Then
                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_G56", m_application, "1", False)
                                        Else
                                            'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_G84", m_application, "1", False)
                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_G84", m_application, "1", False)

                                        End If

                                    End If

                                Else

                                    validar = "No"
                                    If z = "17" Then
                                        If v_sistema = "PSAD-56" Then
                                            cls_catastro.Add_ShapeFile_tmp_reseurba(rese_17_psad_Shapefile, pApp)

                                        Else
                                            cls_catastro.Add_ShapeFile_tmp_reseurba(rese_17_Shapefile, pApp)
                                        End If

                                    ElseIf z = "18" Then
                                        If v_sistema = "PSAD-56" Then
                                            cls_catastro.Add_ShapeFile_tmp_reseurba(rese_18_psad_Shapefile, pApp)
                                        Else
                                            cls_catastro.Add_ShapeFile_tmp_reseurba(rese_18_Shapefile, pApp)
                                        End If

                                    ElseIf z = "19" Then
                                        If v_sistema = "PSAD-56" Then
                                            cls_catastro.Add_ShapeFile_tmp_reseurba(rese_19_psad_Shapefile, pApp)
                                        Else
                                            cls_catastro.Add_ShapeFile_tmp_reseurba(rese_19_Shapefile, pApp)
                                        End If

                                    ElseIf z = "21" Then
                                        If v_sistema = "PSAD-56" Then
                                            cls_catastro.Add_ShapeFile_tmp_reseurba(rese_17_psad_Shapefile, pApp)

                                        End If
                                    ElseIf z = "22" Then
                                        If v_sistema = "PSAD-56" Then
                                            cls_catastro.Add_ShapeFile_tmp_reseurba(rese_18_psad_Shapefile, pApp)

                                        End If

                                    ElseIf z = "23" Then
                                        If v_sistema = "PSAD-56" Then
                                            cls_catastro.Add_ShapeFile_tmp_reseurba(rese_19_psad_Shapefile, pApp)

                                        End If

                                    End If

                                    pQueryFilter = New QueryFilter

                                    pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)

                                    pfeature = pFeatureCursor.NextFeature
                                    'se comento final  parte 1
                                    '  cls_catastro.Actualizar_DM_psad56(v_zona_rese)
                                    cls_catastro.Actualizar_DM(v_zona_rese)

                                    pMap = pMxDoc.FocusMap
                                    pFeatureLayer = pMap.Layer(0)
                                    ' cls_catastro.Borra_Todo_Feature("", pApp)

                                    pMap.DeleteLayer(pFeatureLayer)
                                    pMxDoc.UpdateContents()
                                    If tipo_catanominero = "AREA RESERVADA" Then
                                        ' cls_catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_" & z, m_application, "1", False)
                                        If v_sistema = "PSAD-56" Then
                                            ' cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1 & "D", m_application, "1", False)
                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
                                            'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
                                        Else
                                            ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & z & "W", m_application, "1", False)
                                            ' cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z & "D", m_application, "1", False)
                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z, m_application, "1", False)  'prod

                                            'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z & "D", m_application, "1", False)
                                            'cls_catastro.PT_CargarFeatureClass_SDE("DESA_GIS.GPO_ARE_AREA_RESERVADA_WGS_" & z, m_application, "1", False)
                                        End If

                                    ElseIf tipo_catanominero = "ZONA URBANA" Then
                                        If v_sistema = "PSAD-56" Then

                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_" & z1, m_application, "1", False)
                                        Else
                                            ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)
                                            cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)

                                        End If
                                    End If
                                End If

                                pMap = pMxDoc.FocusMap
                                pFeatureLayer = pMap.Layer(0)

                                pOutFeatureClass = pFeatureLayer.FeatureClass
                                pMap.DeleteLayer(pFeatureLayer)
                                'cls_catastro.Borra_Todo_Feature("", pApp)

                                pMxDoc.UpdateContents()

                                'v_tipoproceso = "INGRESAR"   '''''''''''''''''''''''''  PARA PRUEBA

                                ' End If
                                If v_tipoproceso = "INGRESAR" Then

                                    If validar = "No" Then

                                        If tipo_catanominero = "AREA RESERVADA" Then
                                            pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)

                                            pFeaturepout1 = pFeatureCursor.NextFeature
                                            Do While Not pFeaturepout1 Is Nothing

                                                'cls_catastro.CreaFeature(pOutFeatureClass, pfeature.Shape)
                                                cls_catastro.CreaFeature(pOutFeatureClass, pFeaturepout1.Shape)
                                                pFeaturepout1 = pFeatureCursor.NextFeature
                                            Loop
                                        Else

                                            ' If z <> 24 Then
                                            pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)
                                            pFeaturepout1 = pFeatureCursor.NextFeature
                                            Do While Not pFeaturepout1 Is Nothing
                                                'cls_catastro.CreaFeature(pOutFeatureClass, pfeature.Shape)
                                                cls_catastro.CreaFeature(pOutFeatureClass, pFeaturepout1.Shape)
                                                pFeaturepout1 = pFeatureCursor.NextFeature
                                            Loop

                                            'End If

                                        End If

                                    Else
                                        Dim RetVal
                                        While RetVal Is Nothing
                                            glo_Path_EXE = "U:\DATOS\DBF\"

                                            If tipo_catanominero = "AREA RESERVADA" Then
                                                '   RetVal = Shell(glo_Path_EXE & "apenwgs.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea
                                                RetVal = Shell(glo_Path_EXE & "appenrese_ULT.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea


                                            ElseIf tipo_catanominero = "ZONA URBANA" Then
                                                ' RetVal = Shell(glo_Path_EXE & "appenzur.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea
                                                RetVal = Shell(glo_Path_EXE & "appenzur_ULT.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea

                                            End If

                                        End While
                                    End If

                                ElseIf v_tipoproceso = "MODIFICAR" Then

                                    If validar = "No" Then
                                        If tipo_catanominero = "AREA RESERVADA" Then
                                            pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)

                                            pFeaturepout1 = pFeatureCursor.NextFeature
                                            Do While Not pFeaturepout1 Is Nothing
                                                '  cls_catastro.CreaFeature(pOutFeatureClass, pfeature.Shape)
                                                cls_catastro.CreaFeature(pOutFeatureClass, pFeaturepout1.Shape)
                                                pFeaturepout1 = pFeatureCursor.NextFeature
                                            Loop


                                            pQueryFilter = New QueryFilter
                                            pQueryFilter.WhereClause = "ARCHIVO = '" & v_archivo & "'"
                                            pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
                                            pFeaturepout = pFeatureCursorpout.NextFeature
                                            pFeatureTable = pOutFeatureClass
                                            cls_catastro.Eliminafesture(pFeatureTable, pQueryFilter)

                                        Else
                                            ' If z <> 24 Then

                                            pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)

                                            pFeaturepout1 = pFeatureCursor.NextFeature
                                            Do While Not pFeaturepout1 Is Nothing
                                                '  cls_catastro.CreaFeature(pOutFeatureClass, pfeature.Shape)
                                                cls_catastro.CreaFeature(pOutFeatureClass, pFeaturepout1.Shape)
                                                pFeaturepout1 = pFeatureCursor.NextFeature
                                            Loop

                                            pQueryFilter = New QueryFilter
                                            pQueryFilter.WhereClause = "ARCHIVO = '" & v_archivo & "'"
                                            pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
                                            pFeaturepout = pFeatureCursorpout.NextFeature
                                            pFeatureTable = pOutFeatureClass
                                            cls_catastro.Eliminafesture(pFeatureTable, pQueryFilter)

                                            'End If

                                        End If

                                        'borra el anterior registro por el archivo
                                        '--------------------------------------------

                                        'pQueryFilter = New QueryFilter
                                        'pQueryFilter.WhereClause = "ARCHIVO = '" & v_archivo & "'"
                                        'pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
                                        'pFeaturepout = pFeatureCursorpout.NextFeature
                                        'pFeatureTable = pOutFeatureClass
                                        'cls_catastro.Eliminafesture(pFeatureTable, pQueryFilter)
                                    Else
                                        Try
                                            If tipo_catanominero = "AREA RESERVADA" Then
                                                cls_Oracle.FT_ELIMINA_REG_RESE("1", v_archivo)
                                            Else
                                                'borra versionado zona urbana
                                                cls_Oracle.FT_ELIMINA_REG_RESE("2", v_archivo)
                                            End If

                                        Catch

                                            MsgBox("Error en eliminar registros", MsgBoxStyle.Critical, " SIGCATMIN")
                                        End Try

                                        'inserta mediante proceso

                                        '  pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)

                                        Try
                                            'espera mientras termina para ejecutar siguiente linea

                                            Dim RetVal
                                            While RetVal Is Nothing
                                                glo_Path_EXE = "U:\DATOS\DBF\"

                                                If tipo_catanominero = "AREA RESERVADA" Then
                                                    ' RetVal = Shell(glo_Path_EXE & "apenwgs.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea
                                                    RetVal = Shell(glo_Path_EXE & "appenrese_ULT.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea

                                                ElseIf tipo_catanominero = "ZONA URBANA" Then
                                                    'RetVal = Shell(glo_Path_EXE & "appenzur.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea
                                                    RetVal = Shell(glo_Path_EXE & "appenzur_ULT.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea

                                                End If
                                            End While

                                        Catch ex As Exception
                                            MsgBox("error en proceso python", MsgBoxStyle.Critical, "SIGCAGMIN")

                                        End Try

                                    End If

                                ElseIf v_tipoproceso = "ELIMINAR" Then

                                    'comentado el 22-03-17
                                    Dim clsoracle As New cls_Oracle
                                    Dim lostrSG_D_AREAREG As String = ""
                                    'pQueryFilter = New QueryFilter
                                    'pQueryFilter.WhereClause = "ARCHIVO = '" & v_archivo & "'"
                                    'pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
                                    'pFeaturepout = pFeatureCursorpout.NextFeature
                                    'pFeatureTable = pOutFeatureClass
                                    'cls_catastro.Eliminafesture(pFeatureTable, pQueryFilter)

                                    'se puso esto para borrar
                                    'lostrSG_D_AREAREG = cls_Oracle.FT_ELIMINA_REG_RESE("1", v_archivo)

                                    If tipo_catanominero = "AREA RESERVADA" Then
                                        cls_Oracle.FT_ELIMINA_REG_RESE("1", v_archivo)
                                    Else
                                        'borra versionado zona urbana
                                        cls_Oracle.FT_ELIMINA_REG_RESE("2", v_archivo)
                                    End If

                                End If

                                If validar = "No" Then

                                    If v_tipoproceso = "INGRESAR" Or v_tipoproceso = "MODIFICAR" Then
                                        '  Exit Sub

                                        If (tipo_catanominero = "AREA RESERVADA") Or (tipo_catanominero = "ZONA URBANA") Then
                                            tipo_areas = "ReseUTM"
                                            ''insertando datos al total de reservas
                                            pQueryFilter = New QueryFilter
                                            If tipo_areas = "ReseUTM" Then
                                                'pQueryFilter.WhereClause = "CODIGO = ''"
                                                pQueryFilter.WhereClause = "CODIGO IS NULL"
                                            ElseIf tipo_areas = "ReseGeo" Then
                                                pQueryFilter.WhereClause = "CODIGO IS NULL"
                                            End If

                                            pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
                                            pFeaturepout = pFeatureCursorpout.NextFeature

                                            'empieza a llenar la capa

                                            Do While Not pFeaturepout Is Nothing
                                                If tipo_catanominero = "AREA RESERVADA" Then
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("NOMBRE")) = val_nombre
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("NM_RESE")) = val_nombre & " - " & val_destip
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("CODIGO")) = v_cod_rese

                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)


                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("TP_RESE")) = val_destip

                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)

                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("CATEGORI")) = val_cate
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("CLASE")) = val_nucleo1 ' v_clase_rese
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("ZONA")) = v_zona_rese
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("ZONEX")) = val_zonaexp
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    'pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
                                                    'pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    '   pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = 
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = val_descrinorma & "-" & val_norma

                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    ' pFeaturepout.Value(pFeatureCursorpout.FindField("ARCHIVO")) = v_archivo & "x3"
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("ARCHIVO")) = v_archivo

                                                    'pFeaturepout1.Value(pFeatureCursor.FindField("ARCHIVO")) = v_archivo & "x2"

                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)

                                                    'If pFeatureCursorpout.FindField("FEC_ING") = -1 Then
                                                    '    val_fecing = " "
                                                    'Else
                                                    '    val_fecing = pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_ING")).ToString
                                                    'End If
                                                    '  val_fecing = ""
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_ING")) = val_fecing.ToString
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    '  If val_fecpub <> "" Then
                                                    If IsDate(val_fecpub) = True Then
                                                        pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_PUB")) = val_fecpub
                                                        'pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_PUB")) = val_fecpub.ToString
                                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    End If
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("ENTIDAD")) = val_entidad
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("USO")) = val_uso
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    'pFeaturepout.Value(pFeatureCursorpout.FindField("ESTADO")) = v_estado

                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("EST_GRAF")) = val_estgraf1
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("LEYENDA")) = LEYENDA
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)



                                                    If v_sistema = "PSAD-56" Then
                                                        ' V_area_anm2 = pArea.Area / 10000
                                                        If z = "24" Then
                                                            pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g56
                                                        Else
                                                            pArea = pFeaturepout.Shape
                                                            V_area_anm2 = pArea.Area / 10000
                                                            pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm2
                                                            ' MsgBox(V_area_anm2, MsgBoxStyle.Critical, z)
                                                            If v_zona_rese = z1 Then
                                                                V_area_anm_g56 = V_area_anm2

                                                                V_area_anm_PSAD = V_area_anm_PSAD + V_area_anm2

                                                            End If
                                                        End If


                                                    Else
                                                        '  V_area_anm = pArea.Area / 10000

                                                        If z = "20" Then
                                                            pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g84
                                                        Else
                                                            pArea = pFeaturepout.Shape
                                                            V_area_anm = pArea.Area / 10000
                                                            pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm


                                                            ' MsgBox(V_area_anm, MsgBoxStyle.Critical, z)
                                                            If v_zona_rese = z Then
                                                                V_area_anm_g84 = V_area_anm
                                                                V_area_anm1 = V_area_anm1 + V_area_anm

                                                            End If
                                                        End If

                                                    End If

                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)

                                                    'pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    'pFeaturepout = pFeatureCursorpout.NextFeature
                                                ElseIf tipo_catanominero = "ZONA URBANA" Then
                                                    ' If z <> 24 Then
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("NM_URBA")) = val_nombre & " - " & val_destip
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("CODIGO")) = v_cod_rese
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("NOMBRE")) = val_nombre
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    '   pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = v_area
                                                    '   pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("TP_URBA")) = val_destip
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("CATEGORI")) = val_cate
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("ORDENANZA")) = val_norma

                                                    'descomentar luego si piden descru
                                                    'pFeaturepout.Value(pFeatureCursorpout.FindField("ORDENANZA")) = val_descrinorma.ToString & "-" & val_norma.ToString

                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("ZONA")) = v_zona_rese
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("ZONEX")) = val_zonaexp
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    ' pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
                                                    ' pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    'pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = v_norma
                                                    'pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("ARCHIVO")) = v_archivo
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_ING")) = val_fecing.ToString
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)

                                                    If val_fecpub <> "" Then
                                                        pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_PUB")) = val_fecpub.ToString

                                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    End If
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("ENTIDAD")) = val_entidad
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("USO")) = val_uso
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("EST_GRAF")) = val_estgraf1
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("LEYENDA")) = LEYENDA
                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)



                                                    ' pArea = pFeaturepout.Shape
                                                    ' V_area_anm = pArea.Area / 10000
                                                    'pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm
                                                    If v_sistema = "PSAD-56" Then

                                                        If z = "24" Then
                                                            pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g56
                                                        Else
                                                            pArea = pFeaturepout.Shape
                                                            V_area_anm2 = pArea.Area / 10000
                                                            pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm2

                                                            If v_zona_rese = z1 Then
                                                                V_area_anm_g56 = V_area_anm2
                                                                '  V_area_anm1 = V_area_anm1 + V_area_anm
                                                                V_area_anm_PSAD = V_area_anm_PSAD + V_area_anm2
                                                                ' End If
                                                                ' If v_sistema = "PSAD-56" Then
                                                                'V_area_anm_PSAD = V_area_anm1
                                                                ' Else
                                                                ' V_area_anm1 = V_area_anm1
                                                            End If

                                                        End If

                                                        'End If
                                                    Else




                                                        If z = "20" Then
                                                            pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g84
                                                        Else
                                                            pArea = pFeaturepout.Shape
                                                            V_area_anm = pArea.Area / 10000
                                                            pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm


                                                            ' MsgBox(V_area_anm, MsgBoxStyle.Critical, z)
                                                            If v_zona_rese = z Then
                                                                V_area_anm_g84 = V_area_anm
                                                                V_area_anm1 = V_area_anm1 + V_area_anm

                                                            End If
                                                        End If



                                                    End If

                                                    pFeatureCursorpout.UpdateFeature(pFeaturepout)
                                                End If

                                                ' End If

                                                pFeaturepout = pFeatureCursorpout.NextFeature

                                            Loop
                                            '  pfeature = pFeatureCursor.NextFeature
                                        End If
                                    End If

                                End If  'se puso hoy 21-03-17

                                pMxDoc.UpdateContents()

                            Next z


                            pFeaturepout1 = pFeatureCursor.NextFeature

                            '  Loop

                        Next contador


                    End If

                    '     End If
                    '   Next w
                    '  Next proceso
                    '  MsgBox("TERMIN", MsgBoxStyle.Critical, "GIS")
                    ' Exit Sub
                    '´parte tabular

                    cls_catastro.Borra_Todo_Feature("", m_application)
                    pMxDoc.UpdateContents()


                    For proceso As Integer = 1 To 2

                        'Obteniendo la demarcacion de las areas restringidas que se guardara en tabla temporal

                        If proceso = 1 Then   'PARA PROCESO WGS 84
                            v_sistema = "WGS-84"
                            proceso_arearestrin = "WGS-84"
                            ' MsgBox("actualiza atributos para datum 1984", MsgBoxStyle.Critical, "1")
                        ElseIf proceso = 2 Then  'PARA PROCESO EN PSAD 56 (NO ANAPS)
                            proceso_arearestrin = "PSAD-56"
                            v_sistema = "PSAD-56"
                            '  MsgBox("actualiza atributos para datum 1956", MsgBoxStyle.Critical, "2")
                        End If

                        If v_zona_rese = v_zona_sele Then
                            ' MsgBox(V_area_anm)
                            If v_tipoproceso <> "ELIMINAR" Then
                                Dim lodbtExiste_DM As New DataTable
                                Dim lostrRetorno As String

                                Try
                                    If tipo_catanominero = "AREA RESERVADA" Then
                                        If v_sistema = "PSAD-56" Then
                                            ' lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)

                                            lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "DATA_GIS.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)


                                            'lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "CQUI0543.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)
                                            ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, m_application, "1", False)
                                        Else
                                            'lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)


                                            lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)


                                        End If

                                        '  lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)


                                    Else
                                        'lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_cod_rese)
                                        '   lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)
                                        If v_sistema = "PSAD-56" Then
                                            lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "DATA_GIS.GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)
                                        Else
                                            ' lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)
                                            lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)


                                        End If

                                    End If

                                Catch ex As Exception

                                End Try
                                '      sesion_anm = "999996"
                                '  usuario_anm = "CQUI0543"
                                Dim Cod_dist As String
                                Dim lostrRetorno_eli As String = ""
                                If lodbtExiste_DM.Rows.Count > 0 Then
                                    'Elinando por unica vez la data
                                    Try
                                        lostrRetorno_eli = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, "", "", "", "", v_sistema, "DEL")
                                    Catch ex As Exception
                                        MsgBox("Error en elimiar datos de tabla demarcación", MsgBoxStyle.Critical, "SIGCATMIN")
                                    End Try

                                    For contador1 As Integer = 0 To lodbtExiste_DM.Rows.Count - 1
                                        Cod_dist = lodbtExiste_DM.Rows(contador1).Item("CD_DIST")
                                        If tipo_catanominero = "AREA RESERVADA" Then
                                            '  If v_sistema = "PSAD-56" Then
                                            '  MsgBox("Empezará a actualizar  demarcaciónpara datum " & v_sistema, MsgBoxStyle.Critical, "2")

                                            lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, v_clase_rese, v_sistema, "")

                                            '  MsgBox("Se actualizo atributos de demarcación para datum " & v_sistema, MsgBoxStyle.Critical, "2")
                                            'Else
                                            '   lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, v_clase_rese, v_sistema)
                                            'End If

                                        Else
                                            ' If v_sistema = "PSAD-56" Then
                                            lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, "N", v_sistema, "")
                                            'Else
                                            'lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, "N", v_sistema)
                                            'End If

                                        End If

                                    Next contador1

                                End If
                            End If

                        End If

                        'Obteniendo  las cartas de las Areas Restringidas que se guardara en tabla temporal

                        If v_zona_rese = v_zona_sele Then
                            If v_tipoproceso <> "ELIMINAR" Then
                                Dim lodbtExiste_carta As New DataTable
                                Dim lostrRetorno As String
                                Try
                                    If tipo_catanominero = "AREA RESERVADA" Then
                                        If v_sistema = "PSAD-56" Then
                                            '-comentado esto en 06-09-16 
                                            'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Carta, v_archivo)

                                            'se aplica aparti del 06-09-16 esta linea abajo

                                            '2016
                                            lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "DATA_GIS.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Carta, v_archivo, v_sistema)
                                            'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Carta, v_archivo, v_sistema)

                                        Else

                                            '  lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "CQUI0543." & gstrFC_Carta & v_zona_rese, v_archivo)
                                            '  lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "CQUI0543." & gstrFC_Carta & v_zona_rese, v_archivo)
                                            'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, gstrFC_Carta & v_zona_rese, v_archivo)

                                            '-comentado esto en 06-09-16 

                                            'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo)

                                            'se aplica aparti del 06-09-16 esta linea abajo

                                            'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo, v_sistema)

                                            '2016
                                            lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Carta & "_18", v_archivo, v_sistema)
                                            'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "DATA_GIS." & gstrFC_Carta & "_18", v_archivo, v_sistema)

                                        End If

                                    Else
                                        ' lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_18", gstrFC_Carta, v_archivo)
                                        If v_sistema = "PSAD-56" Then
                                            '-comentado esto en 06-09-16 
                                            ' lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Carta, v_archivo)

                                            'se aplica aparti del 06-09-16 esta linea abajo
                                            lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "DATA_GIS.GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Carta, v_archivo, v_sistema)
                                            'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ZUR_ZONA_URBANA_" & v_zona_rese, "CQUI0543." & gstrFC_Carta, v_archivo, v_sistema)


                                        Else
                                            ' lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "CQUI0543." & gstrFC_Carta & v_zona_rese, v_archivo)
                                            '-comentado esto en 06-09-16 
                                            'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo)
                                            'se aplica aparti del 06-09-16 esta linea abajo

                                            lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Carta & "_18", v_archivo, v_sistema)
                                            'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo, v_sistema)
                                        End If

                                    End If

                                Catch ex As Exception

                                End Try

                                Dim Cod_hoja As String
                                Dim lostrRetorno_eli1 As String = ""
                                If lodbtExiste_carta.Rows.Count > 0 Then
                                    Try
                                        lostrRetorno_eli1 = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, "", "", "", "", v_sistema, "DEL")

                                    Catch ex As Exception
                                        MsgBox("Error en elimiar datos de tabla de cartas", MsgBoxStyle.Critical, "SIGCATMIN")
                                    End Try


                                    For contador1 As Integer = 0 To lodbtExiste_carta.Rows.Count - 1
                                        Cod_hoja = lodbtExiste_carta.Rows(contador1).Item("CD_HOJA")
                                        If tipo_catanominero = "AREA RESERVADA" Then
                                            'lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, v_clase_rese, sesion_anm)
                                            '  If v_sistema = "PSAD-56" Then
                                            '  MsgBox("Empezará a actualizar  carta datum " & v_sistema, MsgBoxStyle.Critical, "2")
                                            lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, v_clase_rese, sesion_anm, v_sistema, "")

                                            ' MsgBox("Se actualizo atributos de cartas para datum " & v_sistema, MsgBoxStyle.Critical, "2")


                                            'Else
                                            '   lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, v_clase_rese, sesion_anm, v_sistema)
                                            ' End If


                                        Else
                                            '   If v_sistema = "PSAD-56" Then
                                            lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, "N", sesion_anm, v_sistema, "")
                                            'Else
                                            ' lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, "N", sesion_anm, v_sistema)
                                            ' End If

                                        End If

                                    Next contador1
                                End If
                            End If
                        End If
                        ' usuario_anm = "cqui0543"

                        If v_zona_rese = v_zona_sele Then
                            If v_tipoproceso <> "ELIMINAR" Then
                                Dim lostrRetorno As String
                                Dim ACCION As String
                                Dim coddata As String
                                If v_tipoproceso = "INGRESAR" Then
                                    ACCION = "INS"
                                ElseIf v_tipoproceso = "MODIFICAR" Then
                                    ACCION = "ACT"
                                Else
                                    ACCION = "DEL"
                                End If
                                If v_sistema = "PSAD-56" Then
                                    coddata = "01"
                                Else
                                    coddata = "02"
                                End If

                                '   V_area_anm1 = Format(Math.Round(V_area_anm1, 4), "###,####.0")

                                If tipo_catanominero = "AREA RESERVADA" Then
                                    ' If v_sistema = "PSAD-56" Then

                                    'SE ACTUALIZO PARA 56 EL INDICADO ACD SOLO PARA QUE ACTUALIZE EL AREA PORQUE YA EN 84 SE CALCULO
                                    If proceso_arearestrin = "PSAD-56" Then
                                        'MsgBox("Se empezará a actualizar el area en 56", MsgBoxStyle.Critical, "1")

                                        ' MsgBox(V_area_anm_PSAD, MsgBoxStyle.Critical, "AREA 56")
                                        V_area_anm_PSAD = Format(Math.Round(V_area_anm_PSAD, 4), "###,###.#000")
                                        ' MsgBox(V_area_anm_PSAD, MsgBoxStyle.Critical, "AREA_56F")
                                        Try
                                            lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("ACD", v_cod_rese, v_clase_rese, V_area_anm_PSAD, usuario_anm, sesion_anm, coddata)
                                            'MsgBox("se actualizo el area en 56", MsgBoxStyle.Critical, "1")
                                        Catch
                                        End Try

                                    Else
                                        'MsgBox(V_area_anm1, MsgBoxStyle.Critical, "AREA")
                                        V_area_anm1 = Format(Math.Round(V_area_anm1, 4), "###,###.#000")
                                        'MsgBox(V_area_anm1, MsgBoxStyle.Critical, "AREA 84 RED")
                                        'MsgBox("Se empezará a actualizar el area en 84", MsgBoxStyle.Critical, "2")
                                        Try
                                            lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("INS", v_cod_rese, v_clase_rese, V_area_anm1, usuario_anm, sesion_anm, coddata)
                                        Catch EX As Exception

                                        End Try
                                        'MsgBox("se actualizo el area en 84", MsgBoxStyle.Critical, "2")
                                        'Else
                                        '   lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm(v_cod_rese, v_clase_rese, V_area_anm, usuario_anm, sesion_anm, v_sistema)
                                        'End If
                                    End If


                                Else
                                    ' If v_sistema = "PSAD-56" Then
                                    'COMENTADO EL 04-11-2016
                                    ' lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm(ACCION, v_cod_rese, "N", V_area_anm1, usuario_anm, sesion_anm, v_sistema)
                                    'Else
                                    'lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm(v_cod_rese, "N", V_area_anm, usuario_anm, sesion_anm, v_sistema)


                                    If proceso_arearestrin = "PSAD-56" Then
                                        V_area_anm_PSAD = Format(Math.Round(V_area_anm_PSAD, 4), "###,###.#000")
                                        lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("ACD", v_cod_rese, "N", V_area_anm_PSAD, usuario_anm, sesion_anm, coddata)
                                    Else
                                        Try
                                            V_area_anm1 = Format(Math.Round(V_area_anm1, 4), "###,###.#000")
                                            lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("INS", v_cod_rese, "N", V_area_anm1, usuario_anm, sesion_anm, coddata)
                                        Catch EX As Exception

                                        End Try


                                    End If


                                End If

                            End If

                        End If
                        '  ElseIf loBoo_flg = False Then
                        ' MsgBox("Seleccione un elemento de la Lista alcanzada", MsgBoxStyle.Information, "SIGCATMIN")
                        'Exit Sub

                        '   End If  'se comento antes estaba
                        'pasa a produccion de tabla temporal a produccion

                        If v_zona_rese = v_zona_sele Then

                            Dim lostrRetorno As String
                            '    Dim mycmd As New oraclec
                            '2016
                            'MsgBox("Se empezará a actualizar tabla en producción para datum" & v_sistema, MsgBoxStyle.Critical, "2")



                            lostrRetorno = cls_Oracle.FT_Actualiza_tabla_temp_prod(v_cod_rese, v_clase_rese, sesion_anm, v_sistema)
                            'MsgBox("Se  actualizo tabla en producción para datum" & v_sistema, MsgBoxStyle.Critical, "2")
                        End If


                    Next proceso
                    'pasa a produccion de tabla temporal a produccion

                    Dim CARPETA As String = ""
                    If tipo_catanominero = "AREA RESERVADA" Then

                        CARPETA = "RESERVAS"
                    ElseIf tipo_catanominero = "ZONA URBANA" Then
                        CARPETA = "URBANAS"
                    End If



                End If  ' se puso

            Next w

            MsgBox("Se ha terminado el proceso satisfactoriamente...", MsgBoxStyle.Information, "SIGCATMIN...")
        Catch ex As Exception
            MsgBox(" Error en procedimiento de actualizacion de AreaS Restringidas", MsgBoxStyle.Critical, "Observacion")

        End Try

        'comentado 31-03-17
        '--------------------------------------------------------------------------------------
        '***************************************************************

        ''Me.Close()
        'Dim cls_catastro As New cls_DM_1
        'Try
        '    Try



        '        cls_catastro.Borra_Todo_Feature("", m_application)
        '        pMxDoc.UpdateContents()

        '        Kill(glo_pathTMP & "\geowgs84.shp")
        '        Kill(glo_pathTMP & "\geowgs84.dbf")
        '        Kill(glo_pathTMP & "\geowgs84.shx")
        '        Kill(glo_pathTMP & "\geowgs84.prj")
        '        Kill(glo_pathTMP & "\geowgs84.sbn")
        '        Kill(glo_pathTMP & "\geowgs84.sbx")


        '        Kill(glo_pathTMP & "\geopasad56.shp")
        '        Kill(glo_pathTMP & "\geopasad56.dbf")
        '        Kill(glo_pathTMP & "\geopasad56.shx")
        '        Kill(glo_pathTMP & "\geopasad56.prj")
        '        Kill(glo_pathTMP & "\geopasad56.sbn")
        '        Kill(glo_pathTMP & "\geopasad56.sbx")
        '        Kill(glo_pathTMP & "\geopasad56.sr.lock")
        '    Catch ex As Exception

        '    End Try


        '    '   usuario_anm = "CQUI0543"
        '    '  pasword_anm = "CQUI0544"
        '    v_sistema = "WGS-84"
        '    Dim cls_Oracle As New cls_Oracle
        '    Dim v_cod_rese As String = ""
        '    '   Dim v_archivo As String = """
        '    Dim v_zona_rese As String = ""
        '    Dim v_clase_rese As String = ""
        '    Dim v_acccion As String = ""
        '    Dim v_clase_sele As String = ""
        '    Dim pFeatureCursor As IFeatureCursor
        '    Dim pfeature As IFeature
        '    Dim pQueryFilter As IQueryFilter

        '    Dim pFeatureTable As ITable
        '    Dim val_nombre As String = ""
        '    Dim val_descrinorma As String = ""

        '    Dim val_fecpub As String = ""
        '    ' Dim val_fecpub1 As String = ""
        '    Dim val_nm_rese As String = ""
        '    Dim val_codtip As String = ""
        '    Dim val_destip As String = ""
        '    Dim val_nucleo As String = ""
        '    Dim val_cate As String = ""
        '    Dim val_nucleo1 As String = ""
        '    Dim val_codcat As String = ""
        '    Dim val_norma As String = ""
        '    Dim val_zonaexp As String = ""
        '    Dim val_fecing As String = ""
        '    Dim val_entidad As String = ""
        '    Dim val_uso As String = ""
        '    Dim val_estgraf As String = ""
        '    Dim val_estgraf1 As String = ""
        '    Dim LEYENDA As String = ""
        '    Dim canti_reg As Integer

        '    Dim V_area_anm As Double
        '    Dim V_area_anm1 As Double
        '    Dim V_area_anm2 As Double
        '    Dim V_area_anm_PSAD As Double
        '    Dim V_area_anm_g56 As Double
        '    Dim V_area_anm_g84 As Double
        '    Dim pFeatureCursorpout As IFeatureCursor
        '    Dim pFeaturepout As IFeature
        '    Dim pFeaturepout1 As IFeature


        '    Dim lodbtExiste_rese As DataTable

        '    Dim loBoo_flg As Boolean = False

        '    ' Dim cls_catastro As New cls_DM_1
        '    Dim cls_eval As New Cls_evaluacion

        '    ' Dim v_tipoproceso As String = "INGRESAR"
        '    '  tipo_catanominero = "AREA RESERVADA"
        '    Dim v_zona_sele As String
        '    Dim val_fecing1 As Date
        '    Dim val_fecing2 As String = "yyyy-mm-dd"
        '    '  Dim val_fecpub1 As Date
        '    Dim val_fecpub1 As String = ""
        '    Dim intArea As Integer
        '    Dim pArea As IArea
        '    Dim v_zona_inicio As String
        '    Dim sele_tipo As String


        '    '  Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
        '    V_area_anm1 = 0
        '    V_area_anm_g84 = 0
        '    V_area_anm_g56 = 0
        '    For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
        '        ' For w As Integer = 0 To Me.dgdDetalle.RowCount - 1

        '        ' For w As Integer = 0 To Me.dgdDetalle.CheckForIllegalCrossThreadCalls - 1

        '        'If Me.dgdDetalle.Item(w, "SELEC") Then  'valida lo seleccionado
        '        If dgdDetalle.Item(w, "SELEC") = True Then
        '            ' v_tipoproceso = "INGRESAR"
        '            loBoo_flg = True




        '            cls_catastro.Borra_Todo_Feature("", m_application)
        '            v_cod_rese = dgdDetalle.Item(w, "CODIGO").ToString

        '            '   tipo_catanominero = Microsoft.VisualBasic.Left(v_cod_rese, 2)
        '            sele_tipo = Microsoft.VisualBasic.Left(v_cod_rese, 2)
        '            If sele_tipo = "ZU" Then

        '                tipo_catanominero = "ZONA URBANA"
        '                nombre_carpeta = "URBANAS"
        '            Else
        '                tipo_catanominero = "AREA RESERVADA"
        '                nombre_carpeta = "RESERVAS"
        '            End If
        '            v_archivo = dgdDetalle.Item(w, "ARCHIVO").ToString.ToUpper
        '            v_clase_rese = dgdDetalle.Item(w, "CLASE").ToString
        '            v_zona_rese = dgdDetalle.Item(w, "ZONA").ToString
        '            v_zona_inicio = v_zona_rese
        '            v_acccion = dgdDetalle.Item(w, "ACCION").ToString

        '            If v_acccion = "INGRESO" Then
        '                v_tipoproceso = "INGRESAR"
        '            ElseIf v_acccion = "MODIFICACION" Then
        '                v_tipoproceso = "MODIFICAR"
        '            ElseIf v_acccion = "ELIMINACION" Then
        '                v_tipoproceso = "ELIMINAR"
        '            End If



        '            If v_zona_rese = "17" Then
        '                rese_17_Shapefile = "Area_" & DateTime.Now.Ticks.ToString
        '                v_archivo_rese = rese_17_Shapefile
        '            ElseIf v_zona_rese = "18" Then
        '                rese_18_Shapefile = "Area_" & DateTime.Now.Ticks.ToString
        '                v_archivo_rese = rese_18_Shapefile
        '            ElseIf v_zona_rese = "19" Then
        '                rese_19_Shapefile = "Area_" & DateTime.Now.Ticks.ToString
        '                v_archivo_rese = rese_19_Shapefile
        '            End If
        '            m_application.Caption = "Procesando Area Restringida:  " & v_cod_rese & "   con Archivo : " & v_archivo.ToString & " en Zona :" & v_zona_rese
        '            Try
        '                lodbtExiste_rese = cls_Oracle.FT_Ver_Area_Restringida(v_cod_rese, v_clase_rese)

        '            Catch ex As Exception
        '                ex.Message.ToString()
        '            End Try

        '            If lodbtExiste_rese.Rows.Count >= 1 Then

        '                ' MsgBox(lodbtExiste_rese.Rows.Count, "X")

        '                For contador As Integer = 0 To lodbtExiste_rese.Rows.Count - 1
        '                    v_cod_rese = lodbtExiste_rese.Rows(contador).Item("CG_CODIGO").ToString
        '                    v_clase_rese = lodbtExiste_rese.Rows(contador).Item("PA_CODPAR").ToString
        '                    v_zona_rese = lodbtExiste_rese.Rows(contador).Item("ZA_ZONA").ToString


        '                    val_nombre = lodbtExiste_rese.Rows(contador).Item("PE_NOMARE").ToString
        '                    val_codtip = lodbtExiste_rese.Rows(contador).Item("Tn_codtip").ToString
        '                    val_destip = lodbtExiste_rese.Rows(contador).Item("tn_destip").ToString

        '                    'Se aumento 2 campos mas para buscar del procedimiento..

        '                    val_descrinorma = lodbtExiste_rese.Rows(contador).Item("NL_DESNOR").ToString

        '                    '  val_fecpub = lodbtExiste_rese.Rows(contador).Item("AN_PUBNOR").ToString
        '                    Try
        '                        val_fecpub1 = lodbtExiste_rese.Rows(contador).Item("AN_PUBNOR")

        '                    Catch ex As Exception
        '                    End Try


        '                    '   If IsDate(val_fecpub1) = True Then

        '                    'val_fecpub = val_fecpub1.ToString
        '                    val_fecpub = val_fecpub1  'mismo formato fecha


        '                    val_cate = lodbtExiste_rese.Rows(contador).Item("CT_DESCRI").ToString
        '                    If (v_clase_rese = "N") Then
        '                        If ((val_cate = "PARQUE NACIONAL") Or (val_cate = "SANTUARIO NACIONAL") Or (val_cate = "SANTUARIO HISTORICO") Or (val_cate = "RESERVA PAISAJISTICA") Or (val_cate = "REFUGIO DE VIDA SILVESTRE") Or (val_cate = "RESERVA NACIONAL") Or (val_cate = "RESERVA COMUNAL") Or (val_cate = "COTO DE CAZA") Or (val_cate = "BOSQUE DE PROTECCION") Or (val_cate = "ZONA RESERVADA")) Then

        '                            val_nucleo1 = "ANP"
        '                        Else
        '                            'val_nucleo1 = "   "
        '                            val_nucleo1 = " "
        '                        End If

        '                    ElseIf (v_clase_rese = "A") Then
        '                        val_nucleo1 = "AMORTIGUAMIENTO"
        '                    End If

        '                    val_codcat = lodbtExiste_rese.Rows(contador).Item("CA_CODCAT").ToString
        '                    val_norma = lodbtExiste_rese.Rows(contador).Item("AN_NORLEG").ToString
        '                    If tipo_catanominero = "AREA RESERVADA" Then

        '                    Else  'ZONA URBANA
        '                        If (val_norma = "") Then
        '                            val_norma = "NO"
        '                        Else
        '                            val_norma = "SI"
        '                        End If
        '                    End If


        '                    val_zonaexp = lodbtExiste_rese.Rows(contador).Item("ZA_ZONA").ToString
        '                    ' val_fecing1 = lodbtExiste_rese.Rows(contador).Item("AN_FECING")
        '                    val_fecing1 = lodbtExiste_rese.Rows(contador).Item("AN_FECING")
        '                    val_entidad = lodbtExiste_rese.Rows(contador).Item("AN_ENTIDAD").ToString
        '                    If IsDate(val_fecing1) = True Then
        '                        val_fecing = val_fecing1.ToString
        '                        'Dim todaysdate As String = String.Format("{dd/MM/yyyy}", DateTime.Now)
        '                        Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
        '                        'Dim todaysdate As String = String.Format("{0:yyyy/MM/dd}", DateTime.Now)
        '                        'val_fecing = val_fecing1.ToString(val_fecing2)
        '                        val_fecing = val_fecing1.ToString(todaysdate)
        '                        ' MsgBox(val_fecing)
        '                    Else
        '                        '  v_fec_denun_x = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
        '                    End If
        '                    '  val_fecing1 = val_fecing
        '                    '    val_fecing = val_fecing1.ToString(val_fecing1)

        '                    If (val_destip = "AREA NATURAL") Then
        '                        If ((val_cate = "RESERVA PAISAJISTICA") Or (val_cate = "REFUGIO DE VIDA SILVESTRE") Or (val_cate = "RESERVA COMUNAL") Or (val_cate = "BOSQUE DE PROTECCION") Or (val_cate = "RESERVA NACIONAL") Or (val_cate = "COTO DE CAZA")) Then
        '                            val_uso = "USO DIRECTO"
        '                        ElseIf ((val_cate = "PARQUE NACIONAL") Or (val_cate = "SANTUARIO NACIONAL") Or (val_cate = "SANTUARIO HISTORICO")) Then
        '                            val_uso = "USO INDIRECTO"
        '                        Else
        '                            val_uso = " "
        '                        End If
        '                    End If

        '                    val_estgraf = lodbtExiste_rese.Rows(contador).Item("AN_ESTGRA").ToString

        '                    If (val_estgraf = "01") Then
        '                        val_estgraf1 = "DEFINTIVO"
        '                        LEYENDA = "DEFINITIVO"
        '                    ElseIf (val_estgraf = "02") Then
        '                        val_estgraf1 = "REFERENCIAL"
        '                        LEYENDA = "REFERENCIAL"
        '                    ElseIf (val_estgraf = "03") Then
        '                        val_estgraf1 = "NO GRAFICA"
        '                        LEYENDA = " "
        '                    ElseIf (val_estgraf = "04") Then
        '                        val_estgraf1 = "PROVISIONAL"
        '                        LEYENDA = "REFERENCIAL"
        '                    Else
        '                        val_estgraf1 = " "
        '                    End If


        '                    cls_catastro.AddFeature_rese(v_archivo, m_application)

        '                    cls_eval.agregacampotema_tpm(v_archivo, "CODIGO")
        '                    cls_eval.consultacapaDM("", "AREAS_RESE", v_archivo)
        '                    arch_cata = "Rese_exp"
        '                    cls_catastro.Exportando_Temas("", v_archivo, pApp)
        '                    cls_catastro.Quitar_Layer(v_archivo, pApp)

        '                    cls_catastro.Add_ShapeFile_tmp_reseurba(v_archivo_rese, pApp)


        '                    cls_eval.agregacampotema_tpm(v_archivo_rese, tipo_catanominero)

        '                    If pFeatureLayer_tmp.FeatureClass.FeatureCount(Nothing) = 0 Then
        '                        MsgBox("No hay registros en la capa ", MsgBoxStyle.Critical, "Observación...")
        '                        Exit Sub
        '                    Else
        '                        canti_reg = pFeatureLayer_tmp.FeatureClass.FeatureCount(Nothing)
        '                    End If

        '                    pQueryFilter = New QueryFilter

        '                    ' pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)

        '                    pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)
        '                    'AQUI NUEVO
        '                    ' pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)


        '                    pFeaturepout1 = pFeatureCursor.NextFeature


        '                    Do While Not pFeaturepout1 Is Nothing
        '                        'Do While pFeaturepout1 Is Nothing

        '                        If v_zona_rese = v_zona_inicio Then
        '                            pArea = pFeaturepout1.Shape
        '                            V_area_anm = pArea.Area / 10000
        '                        End If



        '                        If tipo_catanominero = "AREA RESERVADA" Then

        '                            pFeaturepout1.Value(pFeatureCursor.FindField("NOMBRE")) = val_nombre
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("NM_RESE")) = val_nombre & " - " & val_destip
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("CODIGO")) = v_cod_rese
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("TP_RESE")) = val_destip
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("CATEGORI")) = val_cate
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("CLASE")) = val_nucleo1 ' v_clase_rese
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ZONA")) = v_zona_rese
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ZONEX")) = val_zonaexp
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            'pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
        '                            'pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                            ' pFeaturepout1.Value(pFeatureCursor.FindField("NORMA")) = val_norma
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("NORMA")) = val_descrinorma & "-" & val_norma

        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ARCHIVO")) = v_archivo
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)


        '                            pFeaturepout1.Value(pFeatureCursor.FindField("FEC_ING")) = val_fecing.ToString
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                            ' pFeaturepout1.Value(pFeatureCursor.FindField("FEC_PUB")) = val_fecpub.ToString
        '                            If IsDate(val_fecpub) = True Then


        '                                pFeaturepout1.Value(pFeatureCursor.FindField("FEC_PUB")) = val_fecpub
        '                            End If

        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ENTIDAD")) = val_entidad
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("USO")) = val_uso
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                            pFeaturepout1.Value(pFeatureCursor.FindField("EST_GRAF")) = val_estgraf1
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("LEYENDA")) = LEYENDA
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("HAS")) = V_area_anm


        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)


        '                        ElseIf tipo_catanominero = "ZONA URBANA" Then


        '                            pFeaturepout1.Value(pFeatureCursor.FindField("NM_URBA")) = val_nombre & " - " & val_destip
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("CODIGO")) = v_cod_rese
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("NOMBRE")) = v_nombre
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                            pFeaturepout1.Value(pFeatureCursor.FindField("TP_URBA")) = val_destip
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("CATEGORI")) = val_cate
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            '  pFeaturepout1.Value(pFeatureCursor.FindField("ORDENANZA")) = val_norma
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ORDENANZA")) = val_descrinorma & "-" & val_norma

        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ZONA")) = v_Zona
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ZONEX")) = val_zonaexp
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                            ' pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
        '                            ' pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                            'pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = v_norma
        '                            'pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ARCHIVO")) = v_archivo
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("FEC_ING")) = val_fecing.ToString
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            If IsDate(val_fecpub) = True Then

        '                                pFeaturepout1.Value(pFeatureCursor.FindField("FEC_PUB")) = val_fecpub.ToString
        '                            End If

        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ENTIDAD")) = val_entidad
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("USO")) = val_uso
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("EST_GRAF")) = val_estgraf1
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("LEYENDA")) = LEYENDA
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                            pFeaturepout1.Value(pFeatureCursor.FindField("HAS")) = V_area_anm
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                        End If

        '                        '     End If
        '                        pFeaturepout1 = pFeatureCursor.NextFeature

        '                    Loop


        '                    '***********************se comenta 14-09-16
        '                    '  pfeature = pFeatureCursor.NextFeature

        '                    arch_cata = v_archivo_rese
        '                    cls_catastro.Color_Poligono_Simple(m_application, v_archivo_rese)

        '                    cls_catastro.ShowLabel_DM(v_archivo_rese, m_application)



        '                    Dim v_imagen As String = ""


        '                    'HOY SE CAMBIO RUTA IMAGEN 26-09-16
        '                    ' -----------------
        '                    Dim lostrRetorno As String

        '                    If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & ".bmp") Then

        '                        If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v2.bmp") Then

        '                            If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v3.bmp") Then

        '                                If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v4.bmp") Then

        '                                    If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v5.bmp") Then

        '                                        If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v6.bmp") Then


        '                                        Else
        '                                            v_imagen = v_cod_rese & "v6"
        '                                            cls_catastro.Genera_Imagen_DM(v_cod_rese & "v6", "ANM")
        '                                            lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v6" & ".bmp", usuario_anm, v_sistema)
        '                                        End If
        '                                    Else
        '                                        v_imagen = v_cod_rese & "v5"
        '                                        cls_catastro.Genera_Imagen_DM(v_cod_rese & "v5", "ANM")
        '                                        lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v5" & ".bmp", usuario_anm, v_sistema)
        '                                    End If
        '                                Else
        '                                    v_imagen = v_cod_rese & "v4"
        '                                    cls_catastro.Genera_Imagen_DM(v_cod_rese & "v4", "ANM")
        '                                    lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v4" & ".bmp", usuario_anm, v_sistema)
        '                                End If
        '                            Else
        '                                v_imagen = v_cod_rese & "v3"
        '                                cls_catastro.Genera_Imagen_DM(v_cod_rese & "v3", "ANM")
        '                                lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v3" & ".bmp", usuario_anm, v_sistema)
        '                            End If

        '                        Else
        '                            v_imagen = v_cod_rese & "v2"
        '                            cls_catastro.Genera_Imagen_DM(v_cod_rese & "v2", "ANM")
        '                            lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v2" & ".bmp", usuario_anm, v_sistema)
        '                        End If
        '                    Else
        '                        v_imagen = v_cod_rese
        '                        '  cls_catastro.Genera_Imagen_DM(v_cod_rese & ".bmp", "ANM")
        '                        cls_catastro.Genera_Imagen_DM(v_cod_rese, "ANM")
        '                        lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & ".bmp", usuario_anm, v_sistema)

        '                    End If




        '                    cls_catastro.Limpiar_Texto_Pantalla(m_application)


        '                    'AQUI TERMINO

        '                    pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)
        '                    pfeature = pFeatureCursor.NextFeature
        '                    'se comento final  parte 1
        '                    ' cls_catastro.Actualizar_DM_psad56(v_zona_rese)
        '                    cls_catastro.Actualizar_DM(v_zona_rese)

        '                    If v_zona_rese = "17" Then
        '                        For i As Integer = 1 To 3
        '                            If i = "1" Then
        '                                cls_catastro.proyectacapas("18", v_zona_rese, v_sistema)
        '                            ElseIf i = "2" Then
        '                                cls_catastro.proyectacapas("19", v_zona_rese, v_sistema)
        '                            ElseIf i = "3" Then
        '                                cls_catastro.proyectacapas("GEO", v_zona_rese, v_sistema)
        '                                'ElseIf i = "4" Then
        '                                '    cls_catastro.proyectacapas("GEOPSAD", v_zona_rese, v_sistema)
        '                            End If
        '                        Next i
        '                    ElseIf v_zona_rese = "18" Then
        '                        For i As Integer = 1 To 3
        '                            If i = "1" Then
        '                                cls_catastro.proyectacapas("17", v_zona_rese, v_sistema)
        '                            ElseIf i = "2" Then
        '                                cls_catastro.proyectacapas("19", v_zona_rese, v_sistema)
        '                            ElseIf i = "3" Then
        '                                cls_catastro.proyectacapas("GEO", v_zona_rese, v_sistema)
        '                                'ElseIf i = "4" Then
        '                                '    cls_catastro.proyectacapas("GEOPSAD", v_zona_rese, v_sistema)
        '                            End If
        '                        Next i
        '                    ElseIf v_zona_rese = "19" Then
        '                        For i As Integer = 1 To 3
        '                            If i = "1" Then
        '                                cls_catastro.proyectacapas("17", v_zona_rese, v_sistema)
        '                            ElseIf i = "2" Then
        '                                cls_catastro.proyectacapas("18", v_zona_rese, v_sistema)
        '                            ElseIf i = "3" Then
        '                                cls_catastro.proyectacapas("GEO", v_zona_rese, v_sistema)
        '                                'ElseIf i = "4" Then
        '                                '    cls_catastro.proyectacapas("GEOPSAD", v_zona_rese, v_sistema)
        '                            End If
        '                        Next i
        '                    End If



        '                    If nombre_geowgs_psad = "SI" Then

        '                        'Artificio para proyectar area reservas wgs 84 geodesico a psad 56 geodesico
        '                        '----------------------------------------------------------------------------

        '                        Try
        '                            'Kill(glo_pathTMP & "\geopasad56.shp")
        '                            'Kill(glo_pathTMP & "\geopasad56.dbf")
        '                            'Kill(glo_pathTMP & "\geopasad56.shx")
        '                            'Kill(glo_pathTMP & "\geopasad56.prj")
        '                            'Kill(glo_pathTMP & "\geopasad56.sbn")
        '                            'Kill(glo_pathTMP & "\geopasad56.sbx")

        '                        Catch ex As Exception
        '                        End Try

        '                        Dim RetVal

        '                        While RetVal Is Nothing
        '                            glo_Path_EXE = "U:\DATOS\DBF\"
        '                            'glo_Path_EXE1 = "e:\proyecta\"
        '                            RetVal = Shell(glo_Path_EXE & "proywgs_psad1.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea

        '                            ' Antes ejecutado en net no sale bien la proyeccion en geodesico
        '                            'cls_catastro.proyectacapas_geopasad("GEOPSAD", v_zona_rese, v_sistema)
        '                            '

        '                        End While

        '                        'cls_catastro.Add_ShapeFile_tmp_geodesico("geopasad56", pApp)
        '                        rese_geo_Shapefile_WGEO_PSAD = "geopasad56"
        '                        ' loStrShapefile_cat = "geopasad56x"
        '                        cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile_WGEO_PSAD, pApp)

        '                        ' cls_catastro.Add_ShapeFile_tmp(rese_geo_Shapefile_WGEO_PSAD, pApp)

        '                        '   cls_eval.consultacapaDM(v_cod_rese, "X_capgeo", "geopasad56")


        '                        cls_eval.consultacapaDM("", "AREAS_RESE", rese_geo_Shapefile_WGEO_PSAD)

        '                        arch_cata = "geowgwspsad"
        '                        cls_catastro.Exportando_Temas("", rese_geo_Shapefile_WGEO_PSAD, pApp)





        '                        For i As Integer = 17 To 19
        '                            If i = "17" Then

        '                                cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)

        '                            ElseIf i = "18" Then

        '                                cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)

        '                            ElseIf i = "19" Then

        '                                cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)
        '                            End If
        '                        Next i



        '                    End If



        '                    Exit Sub

        '                    Dim validar As String = ""

        '                    Dim z1 As String

        '                    ' For z As Integer = 17 To 20  'comentado el 24-10-16
        '                    For z As Integer = 17 To 24
        '                        '  For z As Integer = 20 To 20
        '                        '  If (z = "21") Or (z = "22") Or (z = "23") Or (z = "24") Then
        '                        If z = "21" Then       ' ------------------------------------------------------    CAMBIO 21 A 20 PARA PROBAR
        '                            v_sistema = "PSAD-56"
        '                            z1 = "17"
        '                            'z = "21"         ' ------------------  se agrego para probar
        '                            validar = "No"
        '                        ElseIf z = "22" Then
        '                            v_sistema = "PSAD-56"
        '                            z1 = "18"
        '                            validar = "No"
        '                        ElseIf z = "23" Then
        '                            z1 = "19"
        '                            v_sistema = "PSAD-56"
        '                            validar = "No"
        '                        End If

        '                        If v_zona_rese = z Then
        '                            validar = "No"
        '                            pMap = pMxDoc.FocusMap
        '                            pFeatureLayer = pMap.Layer(0)
        '                            '    MsgBox(pFeatureLayer.Name)
        '                            ' cls_catastro.Borra_Todo_Feature("", pApp)

        '                            pMap.DeleteLayer(pFeatureLayer)
        '                            pMxDoc.UpdateContents()
        '                            If tipo_catanominero = "AREA RESERVADA" Then


        '                                ' glo_User_SDE = "CEGO0891"

        '                                If v_sistema = "PSAD-56" Then
        '                                    '  cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1 & "D", m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
        '                                Else
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & z & "W", m_application, "1", False)
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z & "D", m_application, "1", False)

        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z, m_application, "1", False)

        '                                End If


        '                                'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_18", m_application, "1", False)
        '                            ElseIf tipo_catanominero = "ZONA URBANA" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_" & z1, m_application, "1", False)
        '                                Else

        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)
        '                                End If
        '                            End If
        '                            'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z, m_application, "1", False)
        '                            v_zona_sele = v_zona_rese

        '                        ElseIf (z = "20") Or (z = "24") Then

        '                            If z = "20" Then
        '                                validar = "SI geo"
        '                            Else
        '                                validar = "No"
        '                            End If
        '                            If v_sistema = "PSAD-56" Then
        '                                cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile_WGEO_PSAD, pApp)

        '                            Else
        '                                cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile, pApp)
        '                            End If

        '                            '  cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile, pApp)
        '                            pQueryFilter = New QueryFilter
        '                            pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)
        '                            pfeature = pFeatureCursor.NextFeature


        '                            pMap = pMxDoc.FocusMap
        '                            pFeatureLayer = pMap.Layer(0)
        '                            '  cls_catastro.Borra_Todo_Feature("", pApp)

        '                            pMap.DeleteLayer(pFeatureLayer)
        '                            pMxDoc.UpdateContents()
        '                            If tipo_catanominero = "AREA RESERVADA" Then
        '                                'cls_catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_G56", m_application, "1", False)
        '                                If v_sistema = "PSAD-56" Then


        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G56" & "D", m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G56", m_application, "1", False)
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_G56P", m_application, "1", False)
        '                                Else
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_G84", m_application, "1", False)
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G84" & "D", m_application, "1", False) 'SE DESCONEMTO 10-03-17
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G84", m_application, "1", False)  'SE COMENTO HOY 10-03-17
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("DESA_GIS.GPO_ARE_AREA_RESERVADA_G", m_application, "1", False)  'SE COMENTO HOY 10-03-17
        '                                    '  cls_catastro.PT_CargarFeatureClass_SDE("DESA_GIS.GPO_ARE_AREA_RESERVADA_G84D", m_application, "1", False)  'SE COMENTO HOY 10-03-17
        '                                End If



        '                            ElseIf tipo_catanominero = "ZONA URBANA" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_G56", m_application, "1", False)
        '                                Else
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_G84", m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_G84", m_application, "1", False)

        '                                End If

        '                            End If

        '                        Else

        '                            validar = "No"
        '                            If z = "17" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_17_psad_Shapefile, pApp)

        '                                Else
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_17_Shapefile, pApp)
        '                                End If


        '                            ElseIf z = "18" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_18_psad_Shapefile, pApp)
        '                                Else
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_18_Shapefile, pApp)
        '                                End If

        '                            ElseIf z = "19" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_19_psad_Shapefile, pApp)
        '                                Else
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_19_Shapefile, pApp)
        '                                End If

        '                            ElseIf z = "21" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_17_psad_Shapefile, pApp)

        '                                End If
        '                            ElseIf z = "22" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_18_psad_Shapefile, pApp)

        '                                End If

        '                            ElseIf z = "23" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_19_psad_Shapefile, pApp)

        '                                End If

        '                            End If


        '                            pQueryFilter = New QueryFilter

        '                            pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)

        '                            pfeature = pFeatureCursor.NextFeature
        '                            'se comento final  parte 1
        '                            '  cls_catastro.Actualizar_DM_psad56(v_zona_rese)
        '                            cls_catastro.Actualizar_DM(v_zona_rese)

        '                            pMap = pMxDoc.FocusMap
        '                            pFeatureLayer = pMap.Layer(0)
        '                            ' cls_catastro.Borra_Todo_Feature("", pApp)

        '                            pMap.DeleteLayer(pFeatureLayer)
        '                            pMxDoc.UpdateContents()
        '                            If tipo_catanominero = "AREA RESERVADA" Then
        '                                ' cls_catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_" & z, m_application, "1", False)
        '                                If v_sistema = "PSAD-56" Then
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1 & "D", m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
        '                                Else
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & z & "W", m_application, "1", False)
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z & "D", m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z, m_application, "1", False)  'prod

        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z & "D", m_application, "1", False)
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("DESA_GIS.GPO_ARE_AREA_RESERVADA_WGS_" & z, m_application, "1", False)
        '                                End If



        '                            ElseIf tipo_catanominero = "ZONA URBANA" Then
        '                                If v_sistema = "PSAD-56" Then

        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_" & z1, m_application, "1", False)
        '                                Else
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)

        '                                End If
        '                            End If
        '                        End If

        '                        pMap = pMxDoc.FocusMap
        '                        pFeatureLayer = pMap.Layer(0)


        '                        pOutFeatureClass = pFeatureLayer.FeatureClass
        '                        pMap.DeleteLayer(pFeatureLayer)
        '                        'cls_catastro.Borra_Todo_Feature("", pApp)

        '                        pMxDoc.UpdateContents()


        '                        If v_tipoproceso = "INGRESAR" Then

        '                            If validar = "No" Then
        '                                pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)

        '                                pFeaturepout1 = pFeatureCursor.NextFeature
        '                                Do While Not pFeaturepout1 Is Nothing

        '                                    'cls_catastro.CreaFeature(pOutFeatureClass, pfeature.Shape)
        '                                    cls_catastro.CreaFeature(pOutFeatureClass, pFeaturepout1.Shape)
        '                                    pFeaturepout1 = pFeatureCursor.NextFeature
        '                                Loop

        '                            Else

        '                                Dim RetVal
        '                                While RetVal Is Nothing
        '                                    glo_Path_EXE = "U:\DATOS\DBF\"

        '                                    RetVal = Shell(glo_Path_EXE & "apenwgs1.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea
        '                                End While
        '                            End If




        '                        ElseIf v_tipoproceso = "MODIFICAR" Then

        '                            If validar = "No" Then
        '                                pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)


        '                                pFeaturepout1 = pFeatureCursor.NextFeature
        '                                Do While Not pFeaturepout1 Is Nothing
        '                                    '  cls_catastro.CreaFeature(pOutFeatureClass, pfeature.Shape)
        '                                    cls_catastro.CreaFeature(pOutFeatureClass, pFeaturepout1.Shape)
        '                                    pFeaturepout1 = pFeatureCursor.NextFeature
        '                                Loop

        '                                'borra el anterior registro por el archivo
        '                                '--------------------------------------------

        '                                pQueryFilter = New QueryFilter
        '                                pQueryFilter.WhereClause = "ARCHIVO = '" & v_archivo & "'"
        '                                pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
        '                                pFeaturepout = pFeatureCursorpout.NextFeature
        '                                pFeatureTable = pOutFeatureClass
        '                                cls_catastro.Eliminafesture(pFeatureTable, pQueryFilter)
        '                            Else


        '                                'Solo para capa en geodesico
        '                                '**************************************


        '                                'borra archivo de la capa

        '                                Try
        '                                    cls_Oracle.FT_ELIMINA_REG_RESE("1", v_archivo)
        '                                Catch

        '                                    MsgBox("Error en eliminar registros", MsgBoxStyle.Critical, " SIGCATMIN")
        '                                End Try


        '                                'inserta mediante proceso


        '                                '  pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)

        '                                Try


        '                                    'espera mientras termina para ejecutar siguiente linea

        '                                    Dim RetVal
        '                                    While RetVal Is Nothing
        '                                        glo_Path_EXE = "U:\DATOS\DBF\"

        '                                        RetVal = Shell(glo_Path_EXE & "apenwgs1.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea
        '                                    End While



        '                                Catch ex As Exception
        '                                    MsgBox("error en proceso python", MsgBoxStyle.Critical, "SIGCAGMIN")

        '                                End Try

        '                            End If

        '                        ElseIf v_tipoproceso = "ELIMINAR" Then

        '                            'comentado el 22-03-17
        '                            Dim clsoracle As New cls_Oracle
        '                            Dim lostrSG_D_AREAREG As String = ""
        '                            If validar = "No" Then
        '                                pQueryFilter = New QueryFilter
        '                                pQueryFilter.WhereClause = "ARCHIVO = '" & v_archivo & "'"
        '                                pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
        '                                pFeaturepout = pFeatureCursorpout.NextFeature
        '                                pFeatureTable = pOutFeatureClass
        '                                cls_catastro.Eliminafesture(pFeatureTable, pQueryFilter)
        '                            Else
        '                                'se puso esto para borrar
        '                                lostrSG_D_AREAREG = cls_Oracle.FT_ELIMINA_REG_RESE("1", v_archivo)
        '                            End If

        '                        End If


        '                        If validar = "No" Then

        '                            If v_tipoproceso = "INGRESAR" Or v_tipoproceso = "MODIFICAR" Then
        '                                '  Exit Sub
        '                                tipo_areas = "ReseUTM"
        '                                ''insertando datos al total de reservas
        '                                pQueryFilter = New QueryFilter
        '                                If tipo_areas = "ReseUTM" Then
        '                                    'pQueryFilter.WhereClause = "CODIGO = ''"
        '                                    pQueryFilter.WhereClause = "CODIGO IS NULL"
        '                                ElseIf tipo_areas = "ReseGeo" Then
        '                                    pQueryFilter.WhereClause = "CODIGO IS NULL"
        '                                End If

        '                                pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
        '                                pFeaturepout = pFeatureCursorpout.NextFeature

        '                                'empieza a llenar la capa

        '                                Do While Not pFeaturepout Is Nothing
        '                                    If tipo_catanominero = "AREA RESERVADA" Then
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("NOMBRE")) = val_nombre
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("NM_RESE")) = val_nombre & " - " & val_destip
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("CODIGO")) = v_cod_rese

        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)


        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("TP_RESE")) = val_destip

        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("CATEGORI")) = val_cate
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("CLASE")) = val_nucleo1 ' v_clase_rese
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("ZONA")) = v_zona_rese
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("ZONEX")) = val_zonaexp
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        'pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
        '                                        'pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        '   pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = 
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = val_descrinorma & "-" & val_norma

        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        ' pFeaturepout.Value(pFeatureCursorpout.FindField("ARCHIVO")) = v_archivo & "x3"
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("ARCHIVO")) = v_archivo

        '                                        'pFeaturepout1.Value(pFeatureCursor.FindField("ARCHIVO")) = v_archivo & "x2"

        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                        'If pFeatureCursorpout.FindField("FEC_ING") = -1 Then
        '                                        '    val_fecing = " "
        '                                        'Else
        '                                        '    val_fecing = pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_ING")).ToString
        '                                        'End If
        '                                        '  val_fecing = ""
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_ING")) = val_fecing.ToString
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        '  If val_fecpub <> "" Then
        '                                        If IsDate(val_fecpub) = True Then
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_PUB")) = val_fecpub
        '                                            'pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_PUB")) = val_fecpub.ToString
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        End If
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("ENTIDAD")) = val_entidad
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("USO")) = val_uso
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        'pFeaturepout.Value(pFeatureCursorpout.FindField("ESTADO")) = v_estado

        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("EST_GRAF")) = val_estgraf1
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("LEYENDA")) = LEYENDA
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)


        '                                        If v_sistema = "PSAD-56" Then
        '                                            ' V_area_anm2 = pArea.Area / 10000
        '                                            If z = "24" Then
        '                                                pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g56
        '                                            Else
        '                                                pArea = pFeaturepout.Shape
        '                                                V_area_anm2 = pArea.Area / 10000
        '                                                pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm2
        '                                                ' MsgBox(V_area_anm2, MsgBoxStyle.Critical, z)
        '                                                If v_zona_rese = z1 Then
        '                                                    V_area_anm_g56 = V_area_anm2

        '                                                    V_area_anm_PSAD = V_area_anm_PSAD + V_area_anm2

        '                                                End If
        '                                            End If


        '                                        Else
        '                                            '  V_area_anm = pArea.Area / 10000

        '                                            If z = "20" Then
        '                                                pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g84
        '                                            Else
        '                                                pArea = pFeaturepout.Shape
        '                                                V_area_anm = pArea.Area / 10000
        '                                                pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm


        '                                                ' MsgBox(V_area_anm, MsgBoxStyle.Critical, z)
        '                                                If v_zona_rese = z Then
        '                                                    V_area_anm_g84 = V_area_anm
        '                                                    V_area_anm1 = V_area_anm1 + V_area_anm

        '                                                End If
        '                                            End If

        '                                        End If

        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                        'pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        'pFeaturepout = pFeatureCursorpout.NextFeature
        '                                    ElseIf tipo_catanominero = "ZONA URBANA" Then
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("NM_URBA")) = val_nombre & " - " & val_destip
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("CODIGO")) = v_cod_rese
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("NOMBRE")) = v_nombre
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        '   pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = v_area
        '                                        '   pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("TP_URBA")) = val_destip
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("CATEGORI")) = val_cate
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        '  pFeaturepout.Value(pFeatureCursorpout.FindField("ORDENANZA")) = val_norma
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("ORDENANZA")) = val_descrinorma & "-" & val_norma

        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("ZONA")) = v_Zona
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("ZONEX")) = val_zonaexp
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        ' pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
        '                                        ' pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        'pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = v_norma
        '                                        'pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("ARCHIVO")) = v_archivo
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_ING")) = val_fecing.ToString
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                        If val_fecpub <> "" Then
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_PUB")) = val_fecpub.ToString

        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        End If
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("ENTIDAD")) = val_entidad
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("USO")) = val_uso
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("EST_GRAF")) = val_estgraf1
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("LEYENDA")) = LEYENDA
        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                        'pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm
        '                                        'pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                        pArea = pFeaturepout.Shape
        '                                        V_area_anm = pArea.Area / 10000
        '                                        pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm
        '                                        If v_sistema = "PSAD-56" Then

        '                                            If z = "24" Then
        '                                                pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g56
        '                                            Else
        '                                                V_area_anm2 = pArea.Area / 10000
        '                                                pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm2

        '                                                If v_zona_rese = z1 Then
        '                                                    V_area_anm_g56 = V_area_anm2
        '                                                    V_area_anm1 = V_area_anm1 + V_area_anm
        '                                                    ' End If
        '                                                    If v_sistema = "PSAD-56" Then
        '                                                        V_area_anm_PSAD = V_area_anm1
        '                                                    Else
        '                                                        V_area_anm1 = V_area_anm1
        '                                                    End If

        '                                                End If

        '                                            End If
        '                                        Else



        '                                            If z = "20" Then
        '                                                pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g84
        '                                            Else
        '                                                pArea = pFeaturepout.Shape
        '                                                V_area_anm = pArea.Area / 10000
        '                                                pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm


        '                                                ' MsgBox(V_area_anm, MsgBoxStyle.Critical, z)
        '                                                If v_zona_rese = z Then
        '                                                    V_area_anm_g84 = V_area_anm
        '                                                    V_area_anm1 = V_area_anm1 + V_area_anm

        '                                                End If
        '                                            End If



        '                                        End If

        '                                        pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                    End If

        '                                    pFeaturepout = pFeatureCursorpout.NextFeature

        '                                Loop
        '                                '  pfeature = pFeatureCursor.NextFeature
        '                            End If
        '                        End If  'se puso hoy 21-03-17

        '                        pMxDoc.UpdateContents()

        '                    Next z


        '                    pFeaturepout1 = pFeatureCursor.NextFeature

        '                    '  Loop

        '                Next contador


        '            End If

        '            '     End If
        '            '   Next w
        '            '  Next proceso
        '            '  MsgBox("TERMIN", MsgBoxStyle.Critical, "GIS")
        '            ' Exit Sub


        '            For proceso As Integer = 1 To 2

        '                'Obteniendo la demarcacion de las areas restringidas que se guardara en tabla temporal

        '                If proceso = 1 Then   'PARA PROCESO WGS 84
        '                    v_sistema = "WGS-84"
        '                    proceso_arearestrin = "WGS-84"
        '                    ' MsgBox("actualiza atributos para datum 1984", MsgBoxStyle.Critical, "1")
        '                ElseIf proceso = 2 Then  'PARA PROCESO EN PSAD 56 (NO ANAPS)
        '                    proceso_arearestrin = "PSAD-56"
        '                    v_sistema = "PSAD-56"
        '                    '  MsgBox("actualiza atributos para datum 1956", MsgBoxStyle.Critical, "2")
        '                End If

        '                If v_zona_rese = v_zona_sele Then
        '                    ' MsgBox(V_area_anm)
        '                    If v_tipoproceso <> "ELIMINAR" Then
        '                        Dim lodbtExiste_DM As New DataTable
        '                        Dim lostrRetorno As String

        '                        Try
        '                            If tipo_catanominero = "AREA RESERVADA" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    ' lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)

        '                                    lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "DATA_GIS.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)


        '                                    'lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "CQUI0543.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, m_application, "1", False)
        '                                Else
        '                                    'lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)


        '                                    lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)


        '                                End If

        '                                '  lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)


        '                            Else
        '                                'lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_cod_rese)
        '                                '   lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)
        '                                If v_sistema = "PSAD-56" Then
        '                                    lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "DATA_GIS.GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)
        '                                Else
        '                                    ' lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)
        '                                    lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)


        '                                End If

        '                            End If

        '                        Catch ex As Exception

        '                        End Try
        '                        '      sesion_anm = "999996"
        '                        '  usuario_anm = "CQUI0543"
        '                        Dim Cod_dist As String
        '                        Dim lostrRetorno_eli As String = ""
        '                        If lodbtExiste_DM.Rows.Count > 0 Then
        '                            'Elinando por unica vez la data
        '                            Try
        '                                lostrRetorno_eli = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, "", "", "", "", v_sistema, "DEL")
        '                            Catch ex As Exception
        '                                MsgBox("Error en elimiar datos de tabla demarcación", MsgBoxStyle.Critical, "SIGCATMIN")
        '                            End Try




        '                            For contador1 As Integer = 0 To lodbtExiste_DM.Rows.Count - 1
        '                                Cod_dist = lodbtExiste_DM.Rows(contador1).Item("CD_DIST")
        '                                If tipo_catanominero = "AREA RESERVADA" Then
        '                                    '  If v_sistema = "PSAD-56" Then
        '                                    '  MsgBox("Empezará a actualizar  demarcaciónpara datum " & v_sistema, MsgBoxStyle.Critical, "2")

        '                                    lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, v_clase_rese, v_sistema, "")
        '                                    '  MsgBox("Se actualizo atributos de demarcación para datum " & v_sistema, MsgBoxStyle.Critical, "2")
        '                                    'Else
        '                                    '   lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, v_clase_rese, v_sistema)
        '                                    'End If

        '                                Else
        '                                    ' If v_sistema = "PSAD-56" Then
        '                                    lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, "N", v_sistema, "")
        '                                    'Else
        '                                    'lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, "N", v_sistema)
        '                                    'End If

        '                                End If

        '                            Next contador1

        '                        End If
        '                    End If

        '                End If

        '                'Obteniendo  las cartas de las Areas Restringidas que se guardara en tabla temporal

        '                If v_zona_rese = v_zona_sele Then
        '                    If v_tipoproceso <> "ELIMINAR" Then
        '                        Dim lodbtExiste_carta As New DataTable
        '                        Dim lostrRetorno As String
        '                        Try
        '                            If tipo_catanominero = "AREA RESERVADA" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    '-comentado esto en 06-09-16 
        '                                    'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Carta, v_archivo)

        '                                    'se aplica aparti del 06-09-16 esta linea abajo

        '                                    '2016
        '                                    lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "DATA_GIS.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Carta, v_archivo, v_sistema)
        '                                    'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Carta, v_archivo, v_sistema)


        '                                Else

        '                                    '  lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "CQUI0543." & gstrFC_Carta & v_zona_rese, v_archivo)
        '                                    '  lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "CQUI0543." & gstrFC_Carta & v_zona_rese, v_archivo)
        '                                    'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, gstrFC_Carta & v_zona_rese, v_archivo)

        '                                    '-comentado esto en 06-09-16 

        '                                    'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo)

        '                                    'se aplica aparti del 06-09-16 esta linea abajo

        '                                    'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo, v_sistema)

        '                                    '2016
        '                                    lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Carta & "_18", v_archivo, v_sistema)
        '                                    'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "DATA_GIS." & gstrFC_Carta & "_18", v_archivo, v_sistema)

        '                                End If

        '                            Else
        '                                ' lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_18", gstrFC_Carta, v_archivo)
        '                                If v_sistema = "PSAD-56" Then
        '                                    '-comentado esto en 06-09-16 
        '                                    ' lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Carta, v_archivo)

        '                                    'se aplica aparti del 06-09-16 esta linea abajo
        '                                    lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "DATA_GIS.GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Carta, v_archivo, v_sistema)
        '                                    'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ZUR_ZONA_URBANA_" & v_zona_rese, "CQUI0543." & gstrFC_Carta, v_archivo, v_sistema)


        '                                Else
        '                                    ' lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "CQUI0543." & gstrFC_Carta & v_zona_rese, v_archivo)
        '                                    '-comentado esto en 06-09-16 
        '                                    'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo)
        '                                    'se aplica aparti del 06-09-16 esta linea abajo

        '                                    lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Carta & "_18", v_archivo, v_sistema)
        '                                    'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo, v_sistema)
        '                                End If

        '                            End If

        '                        Catch ex As Exception

        '                        End Try

        '                        Dim Cod_hoja As String
        '                        Dim lostrRetorno_eli1 As String = ""
        '                        If lodbtExiste_carta.Rows.Count > 0 Then
        '                            Try
        '                                lostrRetorno_eli1 = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, "", "", "", "", v_sistema, "DEL")

        '                            Catch ex As Exception
        '                                MsgBox("Error en elimiar datos de tabla de cartas", MsgBoxStyle.Critical, "SIGCATMIN")
        '                            End Try


        '                            For contador1 As Integer = 0 To lodbtExiste_carta.Rows.Count - 1
        '                                Cod_hoja = lodbtExiste_carta.Rows(contador1).Item("CD_HOJA")
        '                                If tipo_catanominero = "AREA RESERVADA" Then
        '                                    'lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, v_clase_rese, sesion_anm)
        '                                    '  If v_sistema = "PSAD-56" Then
        '                                    '  MsgBox("Empezará a actualizar  carta datum " & v_sistema, MsgBoxStyle.Critical, "2")
        '                                    lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, v_clase_rese, sesion_anm, v_sistema, "")

        '                                    ' MsgBox("Se actualizo atributos de cartas para datum " & v_sistema, MsgBoxStyle.Critical, "2")


        '                                    'Else
        '                                    '   lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, v_clase_rese, sesion_anm, v_sistema)
        '                                    ' End If


        '                                Else
        '                                    '   If v_sistema = "PSAD-56" Then
        '                                    lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, "N", sesion_anm, v_sistema, "")
        '                                    'Else
        '                                    ' lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, "N", sesion_anm, v_sistema)
        '                                    ' End If

        '                                End If

        '                            Next contador1
        '                        End If
        '                    End If
        '                End If
        '                ' usuario_anm = "cqui0543"

        '                If v_zona_rese = v_zona_sele Then
        '                    If v_tipoproceso <> "ELIMINAR" Then
        '                        Dim lostrRetorno As String
        '                        Dim ACCION As String
        '                        Dim coddata As String
        '                        If v_tipoproceso = "INGRESAR" Then
        '                            ACCION = "INS"
        '                        ElseIf v_tipoproceso = "MODIFICAR" Then
        '                            ACCION = "ACT"
        '                        Else
        '                            ACCION = "DEL"
        '                        End If
        '                        If v_sistema = "PSAD-56" Then
        '                            coddata = "01"
        '                        Else
        '                            coddata = "02"
        '                        End If

        '                        '   V_area_anm1 = Format(Math.Round(V_area_anm1, 4), "###,####.0")

        '                        If tipo_catanominero = "AREA RESERVADA" Then
        '                            ' If v_sistema = "PSAD-56" Then

        '                            'SE ACTUALIZO PARA 56 EL INDICADO ACD SOLO PARA QUE ACTUALIZE EL AREA PORQUE YA EN 84 SE CALCULO
        '                            If proceso_arearestrin = "PSAD-56" Then
        '                                'MsgBox("Se empezará a actualizar el area en 56", MsgBoxStyle.Critical, "1")

        '                                ' MsgBox(V_area_anm_PSAD, MsgBoxStyle.Critical, "AREA 56")
        '                                V_area_anm_PSAD = Format(Math.Round(V_area_anm_PSAD, 4), "###,###.#000")
        '                                ' MsgBox(V_area_anm_PSAD, MsgBoxStyle.Critical, "AREA_56F")
        '                                Try
        '                                    lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("ACD", v_cod_rese, v_clase_rese, V_area_anm_PSAD, usuario_anm, sesion_anm, coddata)
        '                                    'MsgBox("se actualizo el area en 56", MsgBoxStyle.Critical, "1")
        '                                Catch
        '                                End Try

        '                            Else
        '                                'MsgBox(V_area_anm1, MsgBoxStyle.Critical, "AREA")
        '                                V_area_anm1 = Format(Math.Round(V_area_anm1, 4), "###,###.#000")
        '                                'MsgBox(V_area_anm1, MsgBoxStyle.Critical, "AREA 84 RED")
        '                                'MsgBox("Se empezará a actualizar el area en 84", MsgBoxStyle.Critical, "2")
        '                                lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("INS", v_cod_rese, v_clase_rese, V_area_anm1, usuario_anm, sesion_anm, coddata)
        '                                'MsgBox("se actualizo el area en 84", MsgBoxStyle.Critical, "2")
        '                                'Else
        '                                '   lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm(v_cod_rese, v_clase_rese, V_area_anm, usuario_anm, sesion_anm, v_sistema)
        '                                'End If
        '                            End If


        '                        Else
        '                            ' If v_sistema = "PSAD-56" Then
        '                            'COMENTADO EL 04-11-2016
        '                            ' lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm(ACCION, v_cod_rese, "N", V_area_anm1, usuario_anm, sesion_anm, v_sistema)
        '                            'Else
        '                            'lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm(v_cod_rese, "N", V_area_anm, usuario_anm, sesion_anm, v_sistema)


        '                            If proceso_arearestrin = "PSAD-56" Then
        '                                lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("ACD", v_cod_rese, "N", V_area_anm_PSAD, usuario_anm, sesion_anm, coddata)
        '                            Else
        '                                lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("INS", v_cod_rese, "N", V_area_anm1, usuario_anm, sesion_anm, coddata)

        '                            End If


        '                        End If

        '                    End If

        '                End If
        '                '  ElseIf loBoo_flg = False Then
        '                ' MsgBox("Seleccione un elemento de la Lista alcanzada", MsgBoxStyle.Information, "SIGCATMIN")
        '                'Exit Sub

        '                '   End If  'se comento antes estaba
        '                'pasa a produccion de tabla temporal a produccion

        '                If v_zona_rese = v_zona_sele Then

        '                    Dim lostrRetorno As String
        '                    '    Dim mycmd As New oraclec
        '                    '2016
        '                    'MsgBox("Se empezará a actualizar tabla en producción para datum" & v_sistema, MsgBoxStyle.Critical, "2")
        '                    lostrRetorno = cls_Oracle.FT_Actualiza_tabla_temp_prod(v_cod_rese, sesion_anm, v_sistema)
        '                    'MsgBox("Se  actualizo tabla en producción para datum" & v_sistema, MsgBoxStyle.Critical, "2")
        '                End If


        '            Next proceso
        '            'pasa a produccion de tabla temporal a produccion

        '            Dim CARPETA As String = ""
        '            If tipo_catanominero = "AREA RESERVADA" Then

        '                CARPETA = "RESERVAS"
        '            ElseIf tipo_catanominero = "ZONA URBANA" Then
        '                CARPETA = "URBANAS"
        '            End If


        '            ' Exit Sub

        '        End If  ' se puso

        '    Next w




        '    MsgBox("Se ha terminado el proceso satisfactoriamente...", MsgBoxStyle.Information, "SIGCATMIN...")
        'Catch ex As Exception
        '    MsgBox(" Error en procedimiento de actualizacion de AreaS Restringidas", MsgBoxStyle.Critical, "Observacion")

        'End Try



        'comentario de la version 10.1 - 2703-2017


        'Me.Close()
        '  Try
        '    '   usuario_anm = "CQUI0543"
        '    '  pasword_anm = "CQUI0544"
        '    v_sistema = "WGS-84"
        '    Dim cls_Oracle As New cls_Oracle
        '    Dim v_cod_rese As String = ""
        '    '   Dim v_archivo As String = """
        '    Dim v_zona_rese As String = ""
        '    Dim v_clase_rese As String = ""
        '    Dim v_acccion As String = ""
        '    Dim v_clase_sele As String = ""
        '    Dim pFeatureCursor As IFeatureCursor
        '    Dim pfeature As IFeature
        '    Dim pQueryFilter As IQueryFilter

        '    Dim pFeatureTable As ITable
        '    Dim val_nombre As String = ""
        '    Dim val_descrinorma As String = ""

        '    Dim val_fecpub As String = ""
        '    ' Dim val_fecpub1 As String = ""
        '    Dim val_nm_rese As String = ""
        '    Dim val_codtip As String = ""
        '    Dim val_destip As String = ""
        '    Dim val_nucleo As String = ""
        '    Dim val_cate As String = ""
        '    Dim val_nucleo1 As String = ""
        '    Dim val_codcat As String = ""
        '    Dim val_norma As String = ""
        '    Dim val_zonaexp As String = ""
        '    Dim val_fecing As String = ""
        '    Dim val_entidad As String = ""
        '    Dim val_uso As String = ""
        '    Dim val_estgraf As String = ""
        '    Dim val_estgraf1 As String = ""
        '    Dim LEYENDA As String = ""
        '    Dim canti_reg As Integer

        '    Dim V_area_anm As Double
        '    Dim V_area_anm1 As Double
        '    Dim V_area_anm2 As Double
        '    Dim V_area_anm_PSAD As Double
        '    Dim V_area_anm_g56 As Double
        '    Dim V_area_anm_g84 As Double
        '    Dim pFeatureCursorpout As IFeatureCursor
        '    Dim pFeaturepout As IFeature
        '    Dim pFeaturepout1 As IFeature

        '    'Me.dgdDetalle.Columns("SELEC").Caption = "SELEC"
        '    'Me.dgdDetalle.Columns("CONTADOR").Caption = "CONTADOR"
        '    'Me.dgdDetalle.Columns("CODIGO").Caption = "CODIGO"
        '    'Me.dgdDetalle.Columns("ARCHIVO").Caption = "ARCHIVO"
        '    'Me.dgdDetalle.Columns("CLASE").Caption = "CLASE"
        '    'Me.dgdDetalle.Columns("ZONA").Caption = "ZONA"
        '    'Me.dgdDetalle.Columns("ACCION").Caption = "ACCION"
        '    Dim lodbtExiste_rese As DataTable

        '    Dim loBoo_flg As Boolean = False

        '    Dim cls_catastro As New cls_DM_1
        '    Dim cls_eval As New Cls_evaluacion

        '    ' Dim v_tipoproceso As String = "INGRESAR"
        '    '  tipo_catanominero = "AREA RESERVADA"
        '    Dim v_zona_sele As String
        '    Dim val_fecing1 As Date
        '    Dim val_fecing2 As String = "yyyy-mm-dd"
        '    '  Dim val_fecpub1 As Date
        '    Dim val_fecpub1 As String = ""
        '    Dim intArea As Integer
        '    Dim pArea As IArea
        '    Dim v_zona_inicio As String
        '    Dim sele_tipo As String


        '    '  Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
        '    V_area_anm1 = 0
        '    V_area_anm_g84 = 0
        '    V_area_anm_g56 = 0
        '    For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
        '        ' For w As Integer = 0 To Me.dgdDetalle.RowCount - 1

        '        ' For w As Integer = 0 To Me.dgdDetalle.CheckForIllegalCrossThreadCalls - 1

        '        'If Me.dgdDetalle.Item(w, "SELEC") Then  'valida lo seleccionado
        '        If dgdDetalle.Item(w, "SELEC") = True Then
        '            ' v_tipoproceso = "INGRESAR"
        '            loBoo_flg = True


        '            ' For proceso As Integer = 1 To 2


        '            '  If proceso = 1 Then   'PARA PROCESO WGS 84
        '            'v_sistema = "WGS_84"

        '            ' ElseIf proceso = 2 Then  'PARA PROCESO EN PSAD 56 (NO ANAPS)

        '            '   v_sistema = "PSAD-56"
        '            ' End If

        '            cls_catastro.Borra_Todo_Feature("", m_application)
        '            v_cod_rese = dgdDetalle.Item(w, "CODIGO").ToString

        '            '   tipo_catanominero = Microsoft.VisualBasic.Left(v_cod_rese, 2)
        '            sele_tipo = Microsoft.VisualBasic.Left(v_cod_rese, 2)
        '            If sele_tipo = "ZU" Then

        '                tipo_catanominero = "ZONA URBANA"
        '                nombre_carpeta = "URBANAS"
        '            Else
        '                tipo_catanominero = "AREA RESERVADA"
        '                nombre_carpeta = "RESERVAS"
        '            End If
        '            v_archivo = dgdDetalle.Item(w, "ARCHIVO").ToString.ToUpper
        '            v_clase_rese = dgdDetalle.Item(w, "CLASE").ToString
        '            v_zona_rese = dgdDetalle.Item(w, "ZONA").ToString
        '            v_zona_inicio = v_zona_rese
        '            v_acccion = dgdDetalle.Item(w, "ACCION").ToString

        '            If v_acccion = "INGRESO" Then
        '                v_tipoproceso = "INGRESAR"
        '            ElseIf v_acccion = "MODIFICACION" Then
        '                v_tipoproceso = "MODIFICAR"
        '            ElseIf v_acccion = "ELIMINACION" Then
        '                v_tipoproceso = "ELIMINAR"
        '            End If



        '            If v_zona_rese = "17" Then
        '                rese_17_Shapefile = "Area_" & DateTime.Now.Ticks.ToString
        '                v_archivo_rese = rese_17_Shapefile
        '            ElseIf v_zona_rese = "18" Then
        '                rese_18_Shapefile = "Area_" & DateTime.Now.Ticks.ToString
        '                v_archivo_rese = rese_18_Shapefile
        '            ElseIf v_zona_rese = "19" Then
        '                rese_19_Shapefile = "Area_" & DateTime.Now.Ticks.ToString
        '                v_archivo_rese = rese_19_Shapefile
        '            End If
        '            m_application.Caption = "Procesando Area Restringida:  " & v_cod_rese & "   con Archivo : " & v_archivo.ToString & " en Zona :" & v_zona_rese
        '            Try
        '                lodbtExiste_rese = cls_Oracle.FT_Ver_Area_Restringida(v_cod_rese, v_clase_rese)

        '            Catch ex As Exception
        '                ex.Message.ToString()
        '            End Try

        '            If lodbtExiste_rese.Rows.Count >= 1 Then

        '                ' MsgBox(lodbtExiste_rese.Rows.Count, "X")

        '                For contador As Integer = 0 To lodbtExiste_rese.Rows.Count - 1
        '                    v_cod_rese = lodbtExiste_rese.Rows(contador).Item("CG_CODIGO").ToString
        '                    v_clase_rese = lodbtExiste_rese.Rows(contador).Item("PA_CODPAR").ToString
        '                    v_zona_rese = lodbtExiste_rese.Rows(contador).Item("ZA_ZONA").ToString
        '                    'If v_zona_rese = "17" Then
        '                    'v_archivo = lodbtExiste_rese.Rows(contador).Item("AN_ARCZ17").ToString

        '                    'ElseIf v_zona_rese = "18" Then
        '                    'v_archivo = lodbtExiste_rese.Rows(contador).Item("AN_ARCZ18").ToString
        '                    'ElseIf v_zona_rese = "19" Then
        '                    'v_archivo = lodbtExiste_rese.Rows(contador).Item("AN_ARCZ19").ToString
        '                    'End If

        '                    val_nombre = lodbtExiste_rese.Rows(contador).Item("PE_NOMARE").ToString
        '                    val_codtip = lodbtExiste_rese.Rows(contador).Item("Tn_codtip").ToString
        '                    val_destip = lodbtExiste_rese.Rows(contador).Item("tn_destip").ToString

        '                    'Se aumento 2 campos mas para buscar del procedimiento..

        '                    val_descrinorma = lodbtExiste_rese.Rows(contador).Item("NL_DESNOR").ToString

        '                    '  val_fecpub = lodbtExiste_rese.Rows(contador).Item("AN_PUBNOR").ToString
        '                    Try
        '                        val_fecpub1 = lodbtExiste_rese.Rows(contador).Item("AN_PUBNOR")

        '                    Catch ex As Exception
        '                    End Try


        '                    '   If IsDate(val_fecpub1) = True Then
        '                    val_fecpub = val_fecpub1.ToString
        '                    'Dim todaysdate As String = String.Format("{dd/MM/yyyy}", DateTime.Now)
        '                    '  Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
        '                    'Dim todaysdate As String = String.Format("{0:yyyy/MM/dd}", DateTime.Now)
        '                    'val_fecing = val_fecing1.ToString(val_fecing2)
        '                    ' val_fecpub = val_fecpub1.ToString(todaysdate)
        '                    ' MsgBox(val_fecing)
        '                    '  Else
        '                    '  val_fecpub = ""
        '                    '  v_fec_denun_x = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
        '                    '    End If



        '                    ' val_fecpub = val_fecpub.ToString
        '                    '  MsgBox(val_fecpub, MsgBoxStyle.Critical, "x")


        '                    ' Dim vd As DateTime
        '                    '  Exit Sub

        '                    ' vd = (Convert.ToDateTime(val_fecpub))
        '                    '  MsgBox(vd, MsgBoxStyle.Critical, "x1")
        '                    'If IsDate(val_fecpub1) = True Then
        '                    '    val_fecpub = val_fecpub1.ToString

        '                    '    Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}",

        '                    '    val_fecpub = val_fecpub1.ToString((tod)
        '                    'Else
        '                    'End If

        '                    ' val_nucleo = lodbtExiste_rese.Rows(contador).Item("Pa_codpar")
        '                    val_cate = lodbtExiste_rese.Rows(contador).Item("CT_DESCRI").ToString
        '                    If (v_clase_rese = "N") Then
        '                        If ((val_cate = "PARQUE NACIONAL") Or (val_cate = "SANTUARIO NACIONAL") Or (val_cate = "SANTUARIO HISTORICO") Or (val_cate = "RESERVA PAISAJISTICA") Or (val_cate = "REFUGIO DE VIDA SILVESTRE") Or (val_cate = "RESERVA NACIONAL") Or (val_cate = "RESERVA COMUNAL") Or (val_cate = "COTO DE CAZA") Or (val_cate = "BOSQUE DE PROTECCION") Or (val_cate = "ZONA RESERVADA")) Then

        '                            val_nucleo1 = "ANP"
        '                        Else
        '                            'val_nucleo1 = "   "
        '                            val_nucleo1 = " "
        '                        End If

        '                    ElseIf (v_clase_rese = "A") Then
        '                        val_nucleo1 = "AMORTIGUAMIENTO"
        '                    End If

        '                    val_codcat = lodbtExiste_rese.Rows(contador).Item("CA_CODCAT").ToString
        '                    val_norma = lodbtExiste_rese.Rows(contador).Item("AN_NORLEG").ToString
        '                    If tipo_catanominero = "AREA RESERVADA" Then

        '                    Else  'ZONA URBANA
        '                        If (val_norma = "") Then
        '                            val_norma = "NO"
        '                        Else
        '                            val_norma = "SI"
        '                        End If
        '                    End If


        '                    val_zonaexp = lodbtExiste_rese.Rows(contador).Item("ZA_ZONA").ToString
        '                    ' val_fecing1 = lodbtExiste_rese.Rows(contador).Item("AN_FECING")
        '                    val_fecing1 = lodbtExiste_rese.Rows(contador).Item("AN_FECING")
        '                    val_entidad = lodbtExiste_rese.Rows(contador).Item("AN_ENTIDAD").ToString
        '                    If IsDate(val_fecing1) = True Then
        '                        val_fecing = val_fecing1.ToString
        '                        'Dim todaysdate As String = String.Format("{dd/MM/yyyy}", DateTime.Now)
        '                        Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
        '                        'Dim todaysdate As String = String.Format("{0:yyyy/MM/dd}", DateTime.Now)
        '                        'val_fecing = val_fecing1.ToString(val_fecing2)
        '                        val_fecing = val_fecing1.ToString(todaysdate)
        '                        ' MsgBox(val_fecing)
        '                    Else
        '                        '  v_fec_denun_x = MyDate.Day & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & "1501"
        '                    End If
        '                    '  val_fecing1 = val_fecing
        '                    '    val_fecing = val_fecing1.ToString(val_fecing1)



        '                    If (val_destip = "AREA NATURAL") Then
        '                        If ((val_cate = "RESERVA PAISAJISTICA") Or (val_cate = "REFUGIO DE VIDA SILVESTRE") Or (val_cate = "RESERVA COMUNAL") Or (val_cate = "BOSQUE DE PROTECCION") Or (val_cate = "RESERVA NACIONAL") Or (val_cate = "COTO DE CAZA")) Then
        '                            val_uso = "USO DIRECTO"
        '                        ElseIf ((val_cate = "PARQUE NACIONAL") Or (val_cate = "SANTUARIO NACIONAL") Or (val_cate = "SANTUARIO HISTORICO")) Then
        '                            val_uso = "USO INDIRECTO"
        '                        Else
        '                            val_uso = " "
        '                        End If
        '                    End If

        '                    val_estgraf = lodbtExiste_rese.Rows(contador).Item("AN_ESTGRA").ToString

        '                    If (val_estgraf = "01") Then
        '                        val_estgraf1 = "DEFINTIVO"
        '                        LEYENDA = "DEFINITIVO"
        '                    ElseIf (val_estgraf = "02") Then
        '                        val_estgraf1 = "REFERENCIAL"
        '                        LEYENDA = "REFERENCIAL"
        '                    ElseIf (val_estgraf = "03") Then
        '                        val_estgraf1 = "NO GRAFICA"
        '                        LEYENDA = " "
        '                    ElseIf (val_estgraf = "04") Then
        '                        val_estgraf1 = "PROVISIONAL"
        '                        LEYENDA = "REFERENCIAL"
        '                    Else
        '                        val_estgraf1 = " "
        '                    End If

        '                    'If (val_estgraf = "01") Then
        '                    '    LEYENDA = "DEFINITIVO"
        '                    'ElseIf (val_estgraf = "02") Then
        '                    '    LEYENDA = "REFERENCIAL"
        '                    'ElseIf (val_estgraf = "04") Then
        '                    '    LEYENDA = "REFERENCIAL"
        '                    'End If
        '                    ' v_zona_rese = "17"

        '                    '  cls_catastro.Actualizar_DM_psad56(v_zona_rese)
        '                    'Next contador

        '                    ' End If
        '                    'cls_Eval.consultacapaDM("", "LibreDen", "Catastro")

        '                    'cls_catastro.Exportando_Temas("", "Catastro", pApp)
        '                    'cls_catastro.Quitar_Layer("Catastro", pApp)
        '                    ' cls_catastro.AddFeature_rese(v_archivo, m_application)

        '                    cls_catastro.AddFeature_rese(v_archivo, m_application)
        '                    '   Exit Sub
        '                    cls_eval.agregacampotema_tpm(v_archivo, "CODIGO")
        '                    'cls_catastro.Quitar_Layer("Catastro", pApp)
        '                    cls_eval.consultacapaDM("", "AREAS_RESE", v_archivo)
        '                    arch_cata = "Rese_exp"
        '                    cls_catastro.Exportando_Temas("", v_archivo, pApp)
        '                    cls_catastro.Quitar_Layer(v_archivo, pApp)
        '                    '  cls_catastro.Add_ShapeFile_tmp(v_archivo_rese, pApp)
        '                    'se comento esta parte 1
        '                    cls_catastro.Add_ShapeFile_tmp_reseurba(v_archivo_rese, pApp)
        '                    '   Exit Sub

        '                    ''cls_catastro.AddFeature_rese(v_archivo_rese, m_application)
        '                    '' Exit Sub

        '                    cls_eval.agregacampotema_tpm(v_archivo_rese, tipo_catanominero)
        '                    ''Exit Sub


        '                    'Dim canti_reg As Integer

        '                    ''cls_catastro.UpdateValue("", pApp, v_archivo_rese)

        '                    If pFeatureLayer_tmp.FeatureClass.FeatureCount(Nothing) = 0 Then
        '                        MsgBox("No hay registros en la capa ", MsgBoxStyle.Critical, "Observación...")
        '                        Exit Sub
        '                    Else
        '                        canti_reg = pFeatureLayer_tmp.FeatureClass.FeatureCount(Nothing)
        '                    End If
        '                    '  MsgBox(canti_reg)
        '                    pQueryFilter = New QueryFilter

        '                    ' pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)

        '                    pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)
        '                    'AQUI NUEVO
        '                    ' pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)


        '                    pFeaturepout1 = pFeatureCursor.NextFeature


        '                    Do While Not pFeaturepout1 Is Nothing
        '                        'Do While pFeaturepout1 Is Nothing

        '                        If v_zona_rese = v_zona_inicio Then
        '                            pArea = pFeaturepout1.Shape
        '                            V_area_anm = pArea.Area / 10000
        '                        End If


        '                        'If v_zona_rese = v_zona_inicio Then

        '                        '    pArea = pFeaturepout1.Shape
        '                        '    V_area_anm = pArea.Area / 10000

        '                        '    If v_sistema = "PSAD-56" Then
        '                        '        V_area_anm1 = V_area_anm1 + V_area_anm

        '                        '        V_area_anm_PSAD = V_area_anm1
        '                        '    Else
        '                        '        '  V_area_anm1 = V_area_anm1 + V_area_anm
        '                        '        V_area_anm1 = V_area_anm
        '                        '        V_area_anm2 = V_area_anm1
        '                        '    End If
        '                        'End If



        '                        If tipo_catanominero = "AREA RESERVADA" Then

        '                            pFeaturepout1.Value(pFeatureCursor.FindField("NOMBRE")) = val_nombre
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("NM_RESE")) = val_nombre & " - " & val_destip
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("CODIGO")) = v_cod_rese
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("TP_RESE")) = val_destip
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("CATEGORI")) = val_cate
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("CLASE")) = val_nucleo1 ' v_clase_rese
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ZONA")) = v_zona_rese
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ZONEX")) = val_zonaexp
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            'pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
        '                            'pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                            ' pFeaturepout1.Value(pFeatureCursor.FindField("NORMA")) = val_norma
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("NORMA")) = val_descrinorma & "-" & val_norma

        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ARCHIVO")) = v_archivo
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)


        '                            pFeaturepout1.Value(pFeatureCursor.FindField("FEC_ING")) = val_fecing.ToString
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)


        '                            pFeaturepout1.Value(pFeatureCursor.FindField("FEC_PUB")) = val_fecpub.ToString
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ENTIDAD")) = val_entidad
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("USO")) = val_uso
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                            pFeaturepout1.Value(pFeatureCursor.FindField("EST_GRAF")) = val_estgraf1
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("LEYENDA")) = LEYENDA
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("HAS")) = V_area_anm

        '                            'If v_sistema = "PSAD-56" Then

        '                            '    pFeaturepout1.Value(pFeatureCursor.FindField("HAS")) = V_area_anm_PSAD

        '                            'Else
        '                            '    pFeaturepout1.Value(pFeatureCursor.FindField("HAS")) = V_area_anm2
        '                            'End If


        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)


        '                        ElseIf tipo_catanominero = "ZONA URBANA" Then


        '                            pFeaturepout1.Value(pFeatureCursor.FindField("NM_URBA")) = val_nombre & " - " & val_destip
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("CODIGO")) = v_cod_rese
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("NOMBRE")) = v_nombre
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            '   pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = v_area
        '                            '   pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("TP_URBA")) = val_destip
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("CATEGORI")) = val_cate
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            '  pFeaturepout1.Value(pFeatureCursor.FindField("ORDENANZA")) = val_norma
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ORDENANZA")) = val_descrinorma & "-" & val_norma

        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ZONA")) = v_Zona
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ZONEX")) = val_zonaexp
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                            ' pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
        '                            ' pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                            'pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = v_norma
        '                            'pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ARCHIVO")) = v_archivo
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("FEC_ING")) = val_fecing.ToString
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                            pFeaturepout1.Value(pFeatureCursor.FindField("FEC_PUB")) = val_fecpub.ToString
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("ENTIDAD")) = val_entidad
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("USO")) = val_uso
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("EST_GRAF")) = val_estgraf1
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)
        '                            pFeaturepout1.Value(pFeatureCursor.FindField("LEYENDA")) = LEYENDA
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                            pFeaturepout1.Value(pFeatureCursor.FindField("HAS")) = V_area_anm
        '                            pFeatureCursor.UpdateFeature(pFeaturepout1)

        '                            '  pFeaturepout = pFeatureCursorpout.NextFeature
        '                        End If


        '                        '     End If


        '                        pFeaturepout1 = pFeatureCursor.NextFeature
        '                        ' pFeaturepout = pFeatureCursorpout.NextFeature
        '                    Loop


        '                    '***********************se comenta 14-09-16
        '                    '  pfeature = pFeatureCursor.NextFeature

        '                    arch_cata = v_archivo_rese
        '                    cls_catastro.Color_Poligono_Simple(m_application, v_archivo_rese)
        '                    ' cls_catastro.Zoom_to_Layer(v_archivo_rese)
        '                    ' cls_catastro.rotulatexto_dm(v_archivo_rese, pApp)
        '                    cls_catastro.ShowLabel_DM(v_archivo_rese, m_application)

        '                    '  Exit Sub

        '                    'Genera la imagen previa y consultara si existe para guardar en base de datos

        '                    Dim v_imagen As String = ""

        '                    '  pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(ruta_rese & nombre_carpeta & "\", 0)

        '                    '****SE COMENTO ANTIGUO 26-09-16


        '                    'If System.IO.File.Exists(ruta_procesado & "imagen\" & v_cod_rese & ".bmp") Then

        '                    '    If System.IO.File.Exists(ruta_procesado & "imagen\" & v_cod_rese & "v2.bmp") Then

        '                    '        If System.IO.File.Exists(ruta_procesado & "imagen\" & v_cod_rese & "v3.bmp") Then

        '                    '            If System.IO.File.Exists(ruta_procesado & "imagen\" & v_cod_rese & "v4.bmp") Then

        '                    '                If System.IO.File.Exists(ruta_procesado & "imagen\" & v_cod_rese & "v5.bmp") Then

        '                    '                    If System.IO.File.Exists(ruta_procesado & "imagen\" & v_cod_rese & "v6.bmp") Then


        '                    '                    Else
        '                    '                        v_imagen = v_cod_rese & "v6"
        '                    '                        cls_catastro.Genera_Imagen_DM(v_cod_rese & "v6", "ANM")
        '                    '                    End If
        '                    '                Else
        '                    '                    v_imagen = v_cod_rese & "v5"
        '                    '                    cls_catastro.Genera_Imagen_DM(v_cod_rese & "v5", "ANM")
        '                    '                End If
        '                    '            Else
        '                    '                v_imagen = v_cod_rese & "v4"
        '                    '                cls_catastro.Genera_Imagen_DM(v_cod_rese & "v4", "ANM")
        '                    '            End If
        '                    '        Else
        '                    '            v_imagen = v_cod_rese & "v3"
        '                    '            cls_catastro.Genera_Imagen_DM(v_cod_rese & "v3", "ANM")
        '                    '        End If

        '                    '    Else
        '                    '        v_imagen = v_cod_rese & "v2"
        '                    '        cls_catastro.Genera_Imagen_DM(v_cod_rese & "v2", "ANM")
        '                    '    End If
        '                    'Else
        '                    '    v_imagen = v_cod_rese
        '                    '    '  cls_catastro.Genera_Imagen_DM(v_cod_rese & ".bmp", "ANM")
        '                    '    cls_catastro.Genera_Imagen_DM(v_cod_rese, "ANM")
        '                    'End If


        '                    'HOY SE CAMBIO RUTA IMAGEN 26-09-16
        '                    ' -----------------
        '                    Dim lostrRetorno As String

        '                    If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & ".bmp") Then

        '                        If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v2.bmp") Then

        '                            If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v3.bmp") Then

        '                                If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v4.bmp") Then

        '                                    If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v5.bmp") Then

        '                                        If System.IO.File.Exists(ruta_IMAGENES & v_cod_rese & "v6.bmp") Then


        '                                        Else
        '                                            v_imagen = v_cod_rese & "v6"
        '                                            cls_catastro.Genera_Imagen_DM(v_cod_rese & "v6", "ANM")
        '                                            lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v6" & ".bmp", usuario_anm, v_sistema)
        '                                        End If
        '                                    Else
        '                                        v_imagen = v_cod_rese & "v5"
        '                                        cls_catastro.Genera_Imagen_DM(v_cod_rese & "v5", "ANM")
        '                                        lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v5" & ".bmp", usuario_anm, v_sistema)
        '                                    End If
        '                                Else
        '                                    v_imagen = v_cod_rese & "v4"
        '                                    cls_catastro.Genera_Imagen_DM(v_cod_rese & "v4", "ANM")
        '                                    lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v4" & ".bmp", usuario_anm, v_sistema)
        '                                End If
        '                            Else
        '                                v_imagen = v_cod_rese & "v3"
        '                                cls_catastro.Genera_Imagen_DM(v_cod_rese & "v3", "ANM")
        '                                lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v3" & ".bmp", usuario_anm, v_sistema)
        '                            End If

        '                        Else
        '                            v_imagen = v_cod_rese & "v2"
        '                            cls_catastro.Genera_Imagen_DM(v_cod_rese & "v2", "ANM")
        '                            lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & "v2" & ".bmp", usuario_anm, v_sistema)
        '                        End If
        '                    Else
        '                        v_imagen = v_cod_rese
        '                        '  cls_catastro.Genera_Imagen_DM(v_cod_rese & ".bmp", "ANM")
        '                        cls_catastro.Genera_Imagen_DM(v_cod_rese, "ANM")
        '                        lostrRetorno = cls_Oracle.FT_ACTUALIZA_IMAGEM_ANM(v_cod_rese, v_cod_rese & ".bmp", usuario_anm, v_sistema)

        '                    End If




        '                    cls_catastro.Limpiar_Texto_Pantalla(m_application)


        '                    'AQUI TERMINO

        '                    pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)
        '                    pfeature = pFeatureCursor.NextFeature
        '                    'se comento final  parte 1
        '                    ' cls_catastro.Actualizar_DM_psad56(v_zona_rese)
        '                    cls_catastro.Actualizar_DM(v_zona_rese)

        '                    If v_zona_rese = "17" Then
        '                        For i As Integer = 1 To 3
        '                            If i = "1" Then
        '                                cls_catastro.proyectacapas("18", v_zona_rese, v_sistema)
        '                            ElseIf i = "2" Then
        '                                cls_catastro.proyectacapas("19", v_zona_rese, v_sistema)
        '                            ElseIf i = "3" Then
        '                                cls_catastro.proyectacapas("GEO", v_zona_rese, v_sistema)
        '                                'ElseIf i = "4" Then
        '                                '    cls_catastro.proyectacapas("GEOPSAD", v_zona_rese, v_sistema)
        '                            End If
        '                        Next i
        '                    ElseIf v_zona_rese = "18" Then
        '                        For i As Integer = 1 To 3
        '                            If i = "1" Then
        '                                cls_catastro.proyectacapas("17", v_zona_rese, v_sistema)
        '                            ElseIf i = "2" Then
        '                                cls_catastro.proyectacapas("19", v_zona_rese, v_sistema)
        '                            ElseIf i = "3" Then
        '                                cls_catastro.proyectacapas("GEO", v_zona_rese, v_sistema)
        '                                'ElseIf i = "4" Then
        '                                '    cls_catastro.proyectacapas("GEOPSAD", v_zona_rese, v_sistema)
        '                            End If
        '                        Next i
        '                    ElseIf v_zona_rese = "19" Then
        '                        For i As Integer = 1 To 3
        '                            If i = "1" Then
        '                                cls_catastro.proyectacapas("17", v_zona_rese, v_sistema)
        '                            ElseIf i = "2" Then
        '                                cls_catastro.proyectacapas("18", v_zona_rese, v_sistema)
        '                            ElseIf i = "3" Then
        '                                cls_catastro.proyectacapas("GEO", v_zona_rese, v_sistema)
        '                                'ElseIf i = "4" Then
        '                                '    cls_catastro.proyectacapas("GEOPSAD", v_zona_rese, v_sistema)
        '                            End If
        '                        Next i
        '                    End If



        '                    If nombre_geowgs_psad = "SI" Then

        '                        'Artificio para proyectar area reservas wgs 84 geodesico a psad 56 geodesico
        '                        '----------------------------------------------------------------------------

        '                        Try
        '                            Kill(glo_pathTMP & "\geopasad56.shp")
        '                            Kill(glo_pathTMP & "\geopasad56.dbf")
        '                            Kill(glo_pathTMP & "\geopasad56.shx")
        '                            Kill(glo_pathTMP & "\geopasad56.prj")
        '                            Kill(glo_pathTMP & "\geopasad56.sbn")
        '                            Kill(glo_pathTMP & "\geopasad56.sbx")

        '                        Catch ex As Exception
        '                        End Try

        '                        Dim RetVal

        '                        While RetVal Is Nothing
        '                            glo_Path_EXE = "U:\DATOS\DBF\"
        '                            'glo_Path_EXE1 = "e:\proyecta\"
        '                            RetVal = Shell(glo_Path_EXE & "proywgs_psad.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea  'para 10.3


        '                            ' Antes ejecutado en net no sale bien la proyeccion en geodesico
        '                            'cls_catastro.proyectacapas_geopasad("GEOPSAD", v_zona_rese, v_sistema)
        '                            '

        '                        End While

        '                        'cls_catastro.Add_ShapeFile_tmp_geodesico("geopasad56", pApp)
        '                        rese_geo_Shapefile_WGEO_PSAD = "geopasad56"
        '                        '  MsgBox("ENCONTRO", MsgBoxStyle.Critical, "x")


        '                        'For i As Integer = 17 To 19
        '                        '    If i = "17" Then

        '                        '        cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)

        '                        '    ElseIf v_zona_rese = "18" Then

        '                        '        cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)

        '                        '    ElseIf v_zona_rese = "19" Then

        '                        '        cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)
        '                        '    End If
        '                        'Next i

        '                        For i As Integer = 17 To 19
        '                            If i = "17" Then

        '                                cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)

        '                            ElseIf i = "18" Then

        '                                cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)

        '                            ElseIf i = "19" Then

        '                                cls_catastro.proyectacapas_geopasad_UTM(i, v_sistema)
        '                            End If
        '                        Next i



        '                    End If


        '                    'If v_zona_inicio = "17" Then
        '                    '    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_17_psad_Shapefile, pApp)
        '                    'ElseIf v_zona_inicio = "18" Then
        '                    '    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_18_psad_Shapefile, pApp)
        '                    'ElseIf v_zona_inicio = "19" Then
        '                    '    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_19_psad_Shapefile, pApp)
        '                    'End If
        '                    'pMap = pMxDoc.FocusMap
        '                    'pFeatureLayer_tmp = pMap.Layer(0)
        '                    'pQueryFilter = New QueryFilter

        '                    'pFeatureCursor = pFeatureLayer_tmp.Search(Nothing, False)
        '                    'V_area_anm_PSAD = 0
        '                    'pfeature = pFeatureCursor.NextFeature
        '                    'Do While Not pfeature Is Nothing

        '                    '    Try
        '                    '        pArea = pfeature.Shape
        '                    '        '  V_area_anm_g56 = pArea.Area / 10000
        '                    '        V_area_anm_PSAD = pArea.Area / 10000
        '                    '        'ver esta area
        '                    '        '  V_area_anm_PSAD = V_area_anm_PSAD + V_area_anm_g56
        '                    '        '  V_area_anm_PSAD = V_area_anm1

        '                    '    Catch ex As Exception

        '                    '    End Try
        '                    '    pfeature = pFeatureCursor.NextFeature

        '                    'Loop

        '                    'MsgBox(V_area_anm_PSAD)
        '                    '' pMap.DeleteLayer(pFeatureLayer_tmp)

        '                    Dim validar As String = ""

        '                    Dim z1 As String

        '                    ' For z As Integer = 17 To 20  'comentado el 24-10-16
        '                    For z As Integer = 17 To 24
        '                        '  For z As Integer = 20 To 20
        '                        '  If (z = "21") Or (z = "22") Or (z = "23") Or (z = "24") Then
        '                        If z = "21" Then       ' ------------------------------------------------------    CAMBIO 21 A 20 PARA PROBAR
        '                            v_sistema = "PSAD-56"
        '                            z1 = "17"
        '                            'z = "21"         ' ------------------  se agrego para probar
        '                            validar = "No"
        '                        ElseIf z = "22" Then
        '                            v_sistema = "PSAD-56"
        '                            z1 = "18"
        '                            validar = "No"
        '                        ElseIf z = "23" Then
        '                            z1 = "19"
        '                            v_sistema = "PSAD-56"
        '                            validar = "No"
        '                        End If

        '                        If v_zona_rese = z Then
        '                            validar = "No"
        '                            pMap = pMxDoc.FocusMap
        '                            pFeatureLayer = pMap.Layer(0)
        '                            '    MsgBox(pFeatureLayer.Name)
        '                            ' cls_catastro.Borra_Todo_Feature("", pApp)

        '                            pMap.DeleteLayer(pFeatureLayer)
        '                            pMxDoc.UpdateContents()
        '                            If tipo_catanominero = "AREA RESERVADA" Then


        '                                ' glo_User_SDE = "CEGO0891"

        '                                If v_sistema = "PSAD-56" Then
        '                                    '  cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1 & "D", m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
        '                                Else
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & z & "W", m_application, "1", False)
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z & "D", m_application, "1", False)

        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z, m_application, "1", False)

        '                                End If


        '                                'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_18", m_application, "1", False)
        '                            ElseIf tipo_catanominero = "ZONA URBANA" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_" & z1, m_application, "1", False)
        '                                Else

        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)
        '                                End If
        '                            End If
        '                            'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z, m_application, "1", False)
        '                            v_zona_sele = v_zona_rese

        '                        ElseIf (z = "20") Or (z = "24") Then

        '                            If z = "20" Then
        '                                validar = "SI geo"
        '                            Else
        '                                validar = "No"
        '                            End If
        '                            If v_sistema = "PSAD-56" Then
        '                                cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile_WGEO_PSAD, pApp)

        '                            Else
        '                                cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile, pApp)
        '                            End If

        '                            '  cls_catastro.Add_ShapeFile_tmp_reseurba(rese_geo_Shapefile, pApp)
        '                            pQueryFilter = New QueryFilter
        '                            pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)
        '                            pfeature = pFeatureCursor.NextFeature


        '                            pMap = pMxDoc.FocusMap
        '                            pFeatureLayer = pMap.Layer(0)
        '                            '  cls_catastro.Borra_Todo_Feature("", pApp)

        '                            pMap.DeleteLayer(pFeatureLayer)
        '                            pMxDoc.UpdateContents()
        '                            If tipo_catanominero = "AREA RESERVADA" Then
        '                                'cls_catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_G56", m_application, "1", False)
        '                                If v_sistema = "PSAD-56" Then


        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G56" & "D", m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G56", m_application, "1", False)
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_G56P", m_application, "1", False)
        '                                Else
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_G84", m_application, "1", False)
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G84" & "D", m_application, "1", False) 'SE DESCONEMTO 10-03-17
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_G84", m_application, "1", False)  'SE COMENTO HOY 10-03-17
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("DESA_GIS.GPO_ARE_AREA_RESERVADA_G", m_application, "1", False)  'SE COMENTO HOY 10-03-17
        '                                    '  cls_catastro.PT_CargarFeatureClass_SDE("DESA_GIS.GPO_ARE_AREA_RESERVADA_G84D", m_application, "1", False)  'SE COMENTO HOY 10-03-17
        '                                End If



        '                            ElseIf tipo_catanominero = "ZONA URBANA" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_G56", m_application, "1", False)
        '                                Else
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_G84", m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_G84", m_application, "1", False)

        '                                End If

        '                            End If

        '                        Else

        '                            validar = "No"
        '                            If z = "17" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_17_psad_Shapefile, pApp)

        '                                Else
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_17_Shapefile, pApp)
        '                                End If


        '                            ElseIf z = "18" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_18_psad_Shapefile, pApp)
        '                                Else
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_18_Shapefile, pApp)
        '                                End If

        '                            ElseIf z = "19" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_19_psad_Shapefile, pApp)
        '                                Else
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_19_Shapefile, pApp)
        '                                End If

        '                            ElseIf z = "21" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_17_psad_Shapefile, pApp)

        '                                End If
        '                            ElseIf z = "22" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_18_psad_Shapefile, pApp)

        '                                End If

        '                            ElseIf z = "23" Then
        '                                If v_sistema = "PSAD-56" Then
        '                                    cls_catastro.Add_ShapeFile_tmp_reseurba(rese_19_psad_Shapefile, pApp)

        '                                End If

        '                            End If


        '                            pQueryFilter = New QueryFilter

        '                            pFeatureCursor = pFeatureLayer_tmp.Search(pQueryFilter, True)

        '                            pfeature = pFeatureCursor.NextFeature
        '                            'se comento final  parte 1
        '                            '  cls_catastro.Actualizar_DM_psad56(v_zona_rese)
        '                            cls_catastro.Actualizar_DM(v_zona_rese)

        '                            pMap = pMxDoc.FocusMap
        '                            pFeatureLayer = pMap.Layer(0)
        '                            ' cls_catastro.Borra_Todo_Feature("", pApp)

        '                            pMap.DeleteLayer(pFeatureLayer)
        '                            pMxDoc.UpdateContents()
        '                            If tipo_catanominero = "AREA RESERVADA" Then
        '                                ' cls_catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_" & z, m_application, "1", False)
        '                                If v_sistema = "PSAD-56" Then
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1 & "D", m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_" & z1, m_application, "1", False)
        '                                Else
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & z & "W", m_application, "1", False)
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z & "D", m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z, m_application, "1", False)  'prod

        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z & "D", m_application, "1", False)
        '                                    'cls_catastro.PT_CargarFeatureClass_SDE("DESA_GIS.GPO_ARE_AREA_RESERVADA_WGS_" & z, m_application, "1", False)
        '                                End If



        '                            ElseIf tipo_catanominero = "ZONA URBANA" Then
        '                                If v_sistema = "PSAD-56" Then

        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_" & z1, m_application, "1", False)
        '                                Else
        '                                    ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)
        '                                    cls_catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)

        '                                End If
        '                            End If
        '                        End If

        '                        pMap = pMxDoc.FocusMap
        '                        pFeatureLayer = pMap.Layer(0)


        '                        pOutFeatureClass = pFeatureLayer.FeatureClass
        '                        pMap.DeleteLayer(pFeatureLayer)
        '                        'cls_catastro.Borra_Todo_Feature("", pApp)

        '                        pMxDoc.UpdateContents()

        '                        'v_tipoproceso = "INGRESAR"   '''''''''''''''''''''''''  PARA PRUEBA

        '                        ' End If
        '                        If v_tipoproceso = "INGRESAR" Then

        '                            If validar = "No" Then
        '                                pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)

        '                                pFeaturepout1 = pFeatureCursor.NextFeature
        '                                Do While Not pFeaturepout1 Is Nothing

        '                                    'cls_catastro.CreaFeature(pOutFeatureClass, pfeature.Shape)
        '                                    cls_catastro.CreaFeature(pOutFeatureClass, pFeaturepout1.Shape)
        '                                    pFeaturepout1 = pFeatureCursor.NextFeature
        '                                Loop

        '                            Else

        '                                Dim RetVal
        '                                Try
        '                                    While RetVal Is Nothing
        '                                        ' glo_Path_EXE1 = "e:\proyecta\"
        '                                        ' RetVal = Shell(glo_Path_EXE1 & "apenwgs.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea

        '                                        glo_Path_EXE = "U:\DATOS\DBF\"
        '                                        RetVal = Shell(glo_Path_EXE & "apenwgs.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea  'para 10.1



        '                                    End While
        '                                Catch ex As Exception
        '                                    MsgBox("error en proceso python", MsgBoxStyle.Critical, "SIGCAGMIN")

        '                                End Try

        '                            End If



        '                        ElseIf v_tipoproceso = "MODIFICAR" Then  'cuando modifica el area
        '                            'proceso normal sin versionamieto
        '                            If validar = "No" Then
        '                                pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)


        '                                pFeaturepout1 = pFeatureCursor.NextFeature
        '                                Do While Not pFeaturepout1 Is Nothing
        '                                    '  cls_catastro.CreaFeature(pOutFeatureClass, pfeature.Shape)
        '                                    cls_catastro.CreaFeature(pOutFeatureClass, pFeaturepout1.Shape)
        '                                    pFeaturepout1 = pFeatureCursor.NextFeature
        '                                Loop

        '                                'borra el anterior registro por el archivo
        '                                '--------------------------------------------

        '                                pQueryFilter = New QueryFilter
        '                                pQueryFilter.WhereClause = "ARCHIVO = '" & v_archivo & "'"
        '                                pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
        '                                pFeaturepout = pFeatureCursorpout.NextFeature
        '                                pFeatureTable = pOutFeatureClass
        '                                cls_catastro.Eliminafesture(pFeatureTable, pQueryFilter)
        '                            Else


        '                                'Solo para capa en geodesico para versionamiento
        '                                '**************************************

        '                                'Consulta si existe registro antes


        '                                '  Dim cls_oracle As New cls_Oracle

        '                                '  Dim lostrSG_D_REG As String = ""


        '                                ' lostrSG_D_REG = cls_Oracle.FT_SG_CUENTA_REG_RESE(v_archivo, "1")

        '                                'If lostrSG_D_REG = "1" Then

        '                                'borra archivo de la capa

        '                                Try
        '                                    cls_Oracle.FT_ELIMINA_REG_RESE("1", v_archivo)
        '                                Catch

        '                                    MsgBox("Error en eliminar registros", MsgBoxStyle.Critical, " SIGCATMIN")
        '                                End Try


        '                                'inserta mediante proceso


        '                                '  pFeatureCursor = pFeatureLayer_tmp.FeatureClass.Update(Nothing, False)

        '                                Try


        '                                    Dim RetVal

        '                                    While RetVal Is Nothing
        '                                        glo_Path_EXE = "U:\DATOS\DBF\"
        '                                        RetVal = Shell(glo_Path_EXE & "apenwgs.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea  'para 10.1

        '                                        ' glo_Path_EXE1 = "e:\proyecta\"
        '                                        ' RetVal = Shell(glo_Path_EXE1 & "apenwgs.bat", 1, True)  'espera mientras termina para ejecutar siguiente linea  'para 10.3
        '                                    End While
        '                                Catch ex As Exception
        '                                    MsgBox("error en proceso python", MsgBoxStyle.Critical, "SIGCAGMIN")

        '                                End Try


        '                            End If

        '                        ElseIf v_tipoproceso = "ELIMINAR" Then

        '                            'comentado el 22-03-17
        '                            Dim clsoracle As New cls_Oracle
        '                            Dim lostrSG_D_AREAREG As String = ""

        '                            If validar = "No" Then  'proceso normal de eliminación
        '                                pQueryFilter = New QueryFilter
        '                                pQueryFilter.WhereClause = "ARCHIVO = '" & v_archivo & "'"
        '                                pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
        '                                pFeaturepout = pFeatureCursorpout.NextFeature
        '                                pFeatureTable = pOutFeatureClass
        '                                cls_catastro.Eliminafesture(pFeatureTable, pQueryFilter)
        '                            Else

        '                                'se puso esto para borrar en versionamiento
        '                                lostrSG_D_AREAREG = cls_Oracle.FT_ELIMINA_REG_RESE("1", v_archivo)
        '                            End If

        '                        End If


        '                            If validar = "No" Then

        '                                If v_tipoproceso = "INGRESAR" Or v_tipoproceso = "MODIFICAR" Then
        '                                    '  Exit Sub
        '                                    tipo_areas = "ReseUTM"
        '                                    ''insertando datos al total de reservas
        '                                    pQueryFilter = New QueryFilter
        '                                    If tipo_areas = "ReseUTM" Then
        '                                        'pQueryFilter.WhereClause = "CODIGO = ''"
        '                                        pQueryFilter.WhereClause = "CODIGO IS NULL"
        '                                    ElseIf tipo_areas = "ReseGeo" Then
        '                                        pQueryFilter.WhereClause = "CODIGO IS NULL"
        '                                    End If

        '                                    pFeatureCursorpout = pOutFeatureClass.Update(pQueryFilter, True)
        '                                    pFeaturepout = pFeatureCursorpout.NextFeature

        '                                    'empieza a llenar la capa

        '                                    Do While Not pFeaturepout Is Nothing
        '                                        If tipo_catanominero = "AREA RESERVADA" Then
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("NOMBRE")) = val_nombre
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("NM_RESE")) = val_nombre & " - " & val_destip
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("CODIGO")) = v_cod_rese

        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                            '  pFeaturepout.Value(pFeatureCursorpout.FindField("NOMBRE")) = v_nombre
        '                                            ' pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            ' pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = v_area
        '                                            ' pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("TP_RESE")) = val_destip

        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("CATEGORI")) = val_cate
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("CLASE")) = val_nucleo1 ' v_clase_rese
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("ZONA")) = v_zona_rese
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("ZONEX")) = val_zonaexp
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            'pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
        '                                            'pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            '   pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = 
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = val_descrinorma & "-" & val_norma

        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            ' pFeaturepout.Value(pFeatureCursorpout.FindField("ARCHIVO")) = v_archivo & "x3"
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("ARCHIVO")) = v_archivo

        '                                            'pFeaturepout1.Value(pFeatureCursor.FindField("ARCHIVO")) = v_archivo & "x2"

        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                            'If pFeatureCursorpout.FindField("FEC_ING") = -1 Then
        '                                            '    val_fecing = " "
        '                                            'Else
        '                                            '    val_fecing = pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_ING")).ToString
        '                                            'End If
        '                                            '  val_fecing = ""
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_ING")) = val_fecing.ToString
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            If val_fecpub <> "" Then
        '                                                pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_PUB")) = val_fecpub.ToString
        '                                                pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            End If
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("ENTIDAD")) = val_entidad
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("USO")) = val_uso
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            'pFeaturepout.Value(pFeatureCursorpout.FindField("ESTADO")) = v_estado

        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("EST_GRAF")) = val_estgraf1
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("LEYENDA")) = LEYENDA
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                            ' pArea = pFeaturepout.Shape

        '                                            ' MsgBox(z)

        '                                            '  V_area_anm = pArea.Area / 10000
        '                                            ' pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm
        '                                            ' If v_sistema = "PSAD-56" Then
        '                                            '     pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_PSAD
        '                                            ' Else
        '                                            '     pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm2
        '                                            ' End If

        '                                            If v_sistema = "PSAD-56" Then
        '                                                ' V_area_anm2 = pArea.Area / 10000
        '                                                If z = "24" Then
        '                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g56
        '                                                Else
        '                                                    pArea = pFeaturepout.Shape
        '                                                    V_area_anm2 = pArea.Area / 10000
        '                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm2
        '                                                    ' MsgBox(V_area_anm2, MsgBoxStyle.Critical, z)
        '                                                    If v_zona_rese = z1 Then
        '                                                        V_area_anm_g56 = V_area_anm2

        '                                                        V_area_anm_PSAD = V_area_anm_PSAD + V_area_anm2

        '                                                    End If
        '                                                End If


        '                                            Else
        '                                                '  V_area_anm = pArea.Area / 10000

        '                                                If z = "20" Then
        '                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g84
        '                                                Else
        '                                                    pArea = pFeaturepout.Shape
        '                                                    V_area_anm = pArea.Area / 10000
        '                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm


        '                                                    ' MsgBox(V_area_anm, MsgBoxStyle.Critical, z)
        '                                                    If v_zona_rese = z Then
        '                                                        V_area_anm_g84 = V_area_anm
        '                                                        V_area_anm1 = V_area_anm1 + V_area_anm

        '                                                    End If
        '                                                End If

        '                                            End If

        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                            'pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            'pFeaturepout = pFeatureCursorpout.NextFeature
        '                                        ElseIf tipo_catanominero = "ZONA URBANA" Then
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("NM_URBA")) = val_nombre & " - " & val_destip
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("CODIGO")) = v_cod_rese
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("NOMBRE")) = v_nombre
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            '   pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = v_area
        '                                            '   pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("TP_URBA")) = val_destip
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("CATEGORI")) = val_cate
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            '  pFeaturepout.Value(pFeatureCursorpout.FindField("ORDENANZA")) = val_norma
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("ORDENANZA")) = val_descrinorma & "-" & val_norma

        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("ZONA")) = v_Zona
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("ZONEX")) = val_zonaexp
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            ' pFeaturepout.Value(pFeatureCursorpout.FindField("OBS")) = v_obs
        '                                            ' pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            'pFeaturepout.Value(pFeatureCursorpout.FindField("NORMA")) = v_norma
        '                                            'pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("ARCHIVO")) = v_archivo
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_ING")) = val_fecing.ToString
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                            If val_fecpub <> "" Then

        '                                                pFeaturepout.Value(pFeatureCursorpout.FindField("FEC_PUB")) = val_fecpub.ToString
        '                                                pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            End If
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("ENTIDAD")) = val_entidad
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("USO")) = val_uso
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("EST_GRAF")) = val_estgraf1
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("LEYENDA")) = LEYENDA
        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                            'pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm
        '                                            'pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                            pArea = pFeaturepout.Shape
        '                                            V_area_anm = pArea.Area / 10000
        '                                            pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm
        '                                            If v_sistema = "PSAD-56" Then

        '                                                If z = "24" Then
        '                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g56
        '                                                Else
        '                                                    V_area_anm2 = pArea.Area / 10000
        '                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm2

        '                                                    If v_zona_rese = z1 Then
        '                                                        V_area_anm_g56 = V_area_anm2
        '                                                        V_area_anm1 = V_area_anm1 + V_area_anm
        '                                                        ' End If
        '                                                        If v_sistema = "PSAD-56" Then
        '                                                            V_area_anm_PSAD = V_area_anm1
        '                                                        Else
        '                                                            V_area_anm1 = V_area_anm1
        '                                                        End If

        '                                                    End If

        '                                                End If
        '                                            Else

        '                                                'If v_zona_rese = z Then
        '                                                '    V_area_anm1 = V_area_anm1 + V_area_anm

        '                                                '    If v_sistema = "PSAD-56" Then
        '                                                '        V_area_anm_PSAD = V_area_anm1
        '                                                '    Else
        '                                                '        V_area_anm1 = V_area_anm1
        '                                                '    End If


        '                                                'End If


        '                                                If z = "20" Then
        '                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm_g84
        '                                                Else
        '                                                    pArea = pFeaturepout.Shape
        '                                                    V_area_anm = pArea.Area / 10000
        '                                                    pFeaturepout.Value(pFeatureCursorpout.FindField("HAS")) = V_area_anm


        '                                                    ' MsgBox(V_area_anm, MsgBoxStyle.Critical, z)
        '                                                    If v_zona_rese = z Then
        '                                                        V_area_anm_g84 = V_area_anm
        '                                                        V_area_anm1 = V_area_anm1 + V_area_anm

        '                                                    End If
        '                                                End If



        '                                            End If

        '                                            pFeatureCursorpout.UpdateFeature(pFeaturepout)

        '                                        End If

        '                                        pFeaturepout = pFeatureCursorpout.NextFeature

        '                                    Loop
        '                                    '  pfeature = pFeatureCursor.NextFeature
        '                                End If
        '                            End If  'se puso hoy 21-03-17

        '                            pMxDoc.UpdateContents()

        '                    Next z


        'pFeaturepout1 = pFeatureCursor.NextFeature

        ''  Loop

        '                Next contador


        '            End If

        ''     End If
        ''   Next w
        ''  Next proceso
        ''  MsgBox("TERMIN", MsgBoxStyle.Critical, "GIS")
        '' Exit Sub


        'For proceso As Integer = 1 To 2

        '    'Obteniendo la demarcacion de las areas restringidas que se guardara en tabla temporal

        '    If proceso = 1 Then   'PARA PROCESO WGS 84
        '        v_sistema = "WGS-84"
        '        proceso_arearestrin = "WGS-84"
        '        ' MsgBox("actualiza atributos para datum 1984", MsgBoxStyle.Critical, "1")
        '    ElseIf proceso = 2 Then  'PARA PROCESO EN PSAD 56 (NO ANAPS)
        '        proceso_arearestrin = "PSAD-56"
        '        v_sistema = "PSAD-56"
        '        '  MsgBox("actualiza atributos para datum 1956", MsgBoxStyle.Critical, "2")
        '    End If

        '    If v_zona_rese = v_zona_sele Then
        '        ' MsgBox(V_area_anm)
        '        If v_tipoproceso <> "ELIMINAR" Then
        '            Dim lodbtExiste_DM As New DataTable
        '            Dim lostrRetorno As String

        '            Try
        '                If tipo_catanominero = "AREA RESERVADA" Then
        '                    If v_sistema = "PSAD-56" Then
        '                        ' lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)

        '                        lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "DATA_GIS.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)


        '                        'lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "CQUI0543.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)
        '                        ' cls_catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, m_application, "1", False)
        '                    Else
        '                        'lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)


        '                        lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)


        '                    End If

        '                    '  lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)


        '                Else
        '                    'lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_cod_rese)
        '                    '   lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)
        '                    If v_sistema = "PSAD-56" Then
        '                        lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "DATA_GIS.GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Distrito_Z & v_zona_rese, v_archivo)
        '                    Else
        '                        ' lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)
        '                        lodbtExiste_DM = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(32, "GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Distrito_WGS & v_zona_rese, v_archivo)


        '                    End If

        '                End If

        '            Catch ex As Exception

        '            End Try
        '            '      sesion_anm = "999996"
        '            '  usuario_anm = "CQUI0543"
        '            Dim Cod_dist As String
        '            Dim lostrRetorno_eli As String = ""
        '            If lodbtExiste_DM.Rows.Count > 0 Then
        '                'Elinando por unica vez la data
        '                Try
        '                    lostrRetorno_eli = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, "", "", "", "", v_sistema, "DEL")
        '                Catch ex As Exception
        '                    MsgBox("Error en elimiar datos de tabla demarcación", MsgBoxStyle.Critical, "SIGCATMIN")
        '                End Try




        '                For contador1 As Integer = 0 To lodbtExiste_DM.Rows.Count - 1
        '                    Cod_dist = lodbtExiste_DM.Rows(contador1).Item("CD_DIST")
        '                    If tipo_catanominero = "AREA RESERVADA" Then
        '                        '  If v_sistema = "PSAD-56" Then
        '                        '  MsgBox("Empezará a actualizar  demarcaciónpara datum " & v_sistema, MsgBoxStyle.Critical, "2")

        '                        lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, v_clase_rese, v_sistema, "")
        '                        '  MsgBox("Se actualizo atributos de demarcación para datum " & v_sistema, MsgBoxStyle.Critical, "2")
        '                        'Else
        '                        '   lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, v_clase_rese, v_sistema)
        '                        'End If

        '                    Else
        '                        ' If v_sistema = "PSAD-56" Then
        '                        lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, "N", v_sistema, "")
        '                        'Else
        '                        'lostrRetorno = cls_Oracle.FT_Actualizar_DemaXAnm(v_cod_rese, Cod_dist, usuario_anm, sesion_anm, "N", v_sistema)
        '                        'End If

        '                    End If

        '                Next contador1

        '            End If
        '        End If

        '    End If

        '    'Obteniendo  las cartas de las Areas Restringidas que se guardara en tabla temporal

        '    If v_zona_rese = v_zona_sele Then
        '        If v_tipoproceso <> "ELIMINAR" Then
        '            Dim lodbtExiste_carta As New DataTable
        '            Dim lostrRetorno As String
        '            Try
        '                If tipo_catanominero = "AREA RESERVADA" Then
        '                    If v_sistema = "PSAD-56" Then
        '                        '-comentado esto en 06-09-16 
        '                        'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Carta, v_archivo)

        '                        'se aplica aparti del 06-09-16 esta linea abajo

        '                        '2016
        '                        lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "DATA_GIS.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Carta, v_archivo, v_sistema)
        '                        'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ARE_AREA_RESERVADA_" & v_zona_rese, gstrFC_Carta, v_archivo, v_sistema)


        '                    Else

        '                        '  lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "CQUI0543." & gstrFC_Carta & v_zona_rese, v_archivo)
        '                        '  lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "CQUI0543." & gstrFC_Carta & v_zona_rese, v_archivo)
        '                        'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, gstrFC_Carta & v_zona_rese, v_archivo)

        '                        '-comentado esto en 06-09-16 

        '                        'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo)

        '                        'se aplica aparti del 06-09-16 esta linea abajo

        '                        'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo, v_sistema)

        '                        '2016
        '                        lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Carta & "_18", v_archivo, v_sistema)
        '                        'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & v_zona_rese & "W", "DATA_GIS." & gstrFC_Carta & "_18", v_archivo, v_sistema)

        '                    End If

        '                Else
        '                    ' lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_18", gstrFC_Carta, v_archivo)
        '                    If v_sistema = "PSAD-56" Then
        '                        '-comentado esto en 06-09-16 
        '                        ' lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Carta, v_archivo)

        '                        'se aplica aparti del 06-09-16 esta linea abajo
        '                        lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "DATA_GIS.GPO_ZUR_ZONA_URBANA_" & v_zona_rese, gstrFC_Carta, v_archivo, v_sistema)
        '                        'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ZUR_ZONA_URBANA_" & v_zona_rese, "CQUI0543." & gstrFC_Carta, v_archivo, v_sistema)


        '                    Else
        '                        ' lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "CQUI0543." & gstrFC_Carta & v_zona_rese, v_archivo)
        '                        '-comentado esto en 06-09-16 
        '                        'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_1(33, "GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo)
        '                        'se aplica aparti del 06-09-16 esta linea abajo

        '                        lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, "DATA_GIS." & gstrFC_Carta & "_18", v_archivo, v_sistema)
        '                        'lodbtExiste_carta = cls_Oracle.FT_Intersecta_Fclass_Oracle_capas(v_zona_rese, "CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & v_zona_rese, gstrFC_Carta & "_18", v_archivo, v_sistema)
        '                    End If

        '                End If

        '            Catch ex As Exception

        '            End Try

        '            Dim Cod_hoja As String
        '            Dim lostrRetorno_eli1 As String = ""
        '            If lodbtExiste_carta.Rows.Count > 0 Then
        '                Try
        '                    lostrRetorno_eli1 = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, "", "", "", "", v_sistema, "DEL")

        '                Catch ex As Exception
        '                    MsgBox("Error en elimiar datos de tabla de cartas", MsgBoxStyle.Critical, "SIGCATMIN")
        '                End Try


        '                For contador1 As Integer = 0 To lodbtExiste_carta.Rows.Count - 1
        '                    Cod_hoja = lodbtExiste_carta.Rows(contador1).Item("CD_HOJA")
        '                    If tipo_catanominero = "AREA RESERVADA" Then
        '                        'lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, v_clase_rese, sesion_anm)
        '                        '  If v_sistema = "PSAD-56" Then
        '                        '  MsgBox("Empezará a actualizar  carta datum " & v_sistema, MsgBoxStyle.Critical, "2")
        '                        lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, v_clase_rese, sesion_anm, v_sistema, "")

        '                        ' MsgBox("Se actualizo atributos de cartas para datum " & v_sistema, MsgBoxStyle.Critical, "2")


        '                        'Else
        '                        '   lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, v_clase_rese, sesion_anm, v_sistema)
        '                        ' End If


        '                    Else
        '                        '   If v_sistema = "PSAD-56" Then
        '                        lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, "N", sesion_anm, v_sistema, "")
        '                        'Else
        '                        ' lostrRetorno = cls_Oracle.FT_Actualizar_cartaXAnm(v_cod_rese, Cod_hoja, usuario_anm, "N", sesion_anm, v_sistema)
        '                        ' End If

        '                    End If

        '                Next contador1
        '            End If
        '        End If
        '    End If
        '    ' usuario_anm = "cqui0543"

        '    If v_zona_rese = v_zona_sele Then
        '        If v_tipoproceso <> "ELIMINAR" Then
        '            Dim lostrRetorno As String
        '            Dim ACCION As String
        '            Dim coddata As String
        '            If v_tipoproceso = "INGRESAR" Then
        '                ACCION = "INS"
        '            ElseIf v_tipoproceso = "MODIFICAR" Then
        '                ACCION = "ACT"
        '            Else
        '                ACCION = "DEL"
        '            End If
        '            If v_sistema = "PSAD-56" Then
        '                coddata = "01"
        '            Else
        '                coddata = "02"
        '            End If

        '            '   V_area_anm1 = Format(Math.Round(V_area_anm1, 4), "###,####.0")

        '            If tipo_catanominero = "AREA RESERVADA" Then
        '                ' If v_sistema = "PSAD-56" Then

        '                'SE ACTUALIZO PARA 56 EL INDICADO ACD SOLO PARA QUE ACTUALIZE EL AREA PORQUE YA EN 84 SE CALCULO
        '                If proceso_arearestrin = "PSAD-56" Then
        '                    'MsgBox("Se empezará a actualizar el area en 56", MsgBoxStyle.Critical, "1")

        '                    ' MsgBox(V_area_anm_PSAD, MsgBoxStyle.Critical, "AREA 56")
        '                    V_area_anm_PSAD = Format(Math.Round(V_area_anm_PSAD, 4), "###,###.#000")
        '                    ' MsgBox(V_area_anm_PSAD, MsgBoxStyle.Critical, "AREA_56F")
        '                    Try
        '                        lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("ACD", v_cod_rese, v_clase_rese, V_area_anm_PSAD, usuario_anm, sesion_anm, coddata)
        '                        'MsgBox("se actualizo el area en 56", MsgBoxStyle.Critical, "1")
        '                    Catch
        '                    End Try

        '                Else
        '                    'MsgBox(V_area_anm1, MsgBoxStyle.Critical, "AREA")
        '                    V_area_anm1 = Format(Math.Round(V_area_anm1, 4), "###,###.#000")
        '                    'MsgBox(V_area_anm1, MsgBoxStyle.Critical, "AREA 84 RED")
        '                    'MsgBox("Se empezará a actualizar el area en 84", MsgBoxStyle.Critical, "2")
        '                    lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("INS", v_cod_rese, v_clase_rese, V_area_anm1, usuario_anm, sesion_anm, coddata)
        '                    'MsgBox("se actualizo el area en 84", MsgBoxStyle.Critical, "2")
        '                    'Else
        '                    '   lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm(v_cod_rese, v_clase_rese, V_area_anm, usuario_anm, sesion_anm, v_sistema)
        '                    'End If
        '                End If


        '            Else
        '                ' If v_sistema = "PSAD-56" Then
        '                'COMENTADO EL 04-11-2016
        '                ' lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm(ACCION, v_cod_rese, "N", V_area_anm1, usuario_anm, sesion_anm, v_sistema)
        '                'Else
        '                'lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm(v_cod_rese, "N", V_area_anm, usuario_anm, sesion_anm, v_sistema)


        '                If proceso_arearestrin = "PSAD-56" Then
        '                    lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("ACD", v_cod_rese, "N", V_area_anm_PSAD, usuario_anm, sesion_anm, coddata)
        '                Else
        '                    lostrRetorno = cls_Oracle.FT_Actualizar_AreasXAnm("INS", v_cod_rese, "N", V_area_anm1, usuario_anm, sesion_anm, coddata)

        '                End If


        '            End If

        '        End If

        '    End If
        '    '  ElseIf loBoo_flg = False Then
        '    ' MsgBox("Seleccione un elemento de la Lista alcanzada", MsgBoxStyle.Information, "SIGCATMIN")
        '    'Exit Sub

        '    '   End If  'se comento antes estaba
        '    'pasa a produccion de tabla temporal a produccion

        '    If v_zona_rese = v_zona_sele Then

        '        Dim lostrRetorno As String
        '        '    Dim mycmd As New oraclec
        '        '2016
        '        'MsgBox("Se empezará a actualizar tabla en producción para datum" & v_sistema, MsgBoxStyle.Critical, "2")
        '        lostrRetorno = cls_Oracle.FT_Actualiza_tabla_temp_prod(v_cod_rese, sesion_anm, v_sistema)
        '        'MsgBox("Se  actualizo tabla en producción para datum" & v_sistema, MsgBoxStyle.Critical, "2")
        '    End If


        'Next proceso
        ''pasa a produccion de tabla temporal a produccion

        'Dim CARPETA As String = ""
        'If tipo_catanominero = "AREA RESERVADA" Then

        '    CARPETA = "RESERVAS"
        'ElseIf tipo_catanominero = "ZONA URBANA" Then
        '    CARPETA = "URBANAS"
        'End If

        ''Se comentara el 03-01-17 porque ya no se copiara a carpeta procesado
        ''****************************************************************************


        ''If v_zona_rese = v_zona_sele Then  'copia poligono generado a carpeta procesado

        ''    Dim ruta1 As String
        ''    Dim ruta2 As String
        ''    If v_tipoproceso <> "ELIMINAR" Then

        ''        ruta1 = glo_pathTMP & "\" & v_archivo_rese & ".shp"
        ''        If v_sistema = "PSAD-56" Then
        ''            ruta2 = ruta_procesado & CARPETA & "\" & v_archivo & ".shp"
        ''        Else
        ''            ruta2 = ruta_procesado & CARPETA & "\wgs84\" & v_archivo & ".shp"
        ''        End If


        ''        System.IO.File.Copy(ruta1, ruta2, True)
        ''        ruta1 = glo_pathTMP & "\" & v_archivo_rese & ".shx"
        ''        If v_sistema = "PSAD-56" Then
        ''            ruta2 = ruta_procesado & CARPETA & "\" & v_archivo & ".shx"
        ''        Else
        ''            ruta2 = ruta_procesado & CARPETA & "\wgs84\" & v_archivo & ".shx"
        ''        End If

        ''        System.IO.File.Copy(ruta1, ruta2, True)


        ''        ruta1 = glo_pathTMP & "\" & v_archivo_rese & ".dbf"
        ''        If v_sistema = "PSAD-56" Then
        ''            ruta2 = ruta_procesado & CARPETA & "\" & v_archivo & ".dbf"
        ''        Else
        ''            ruta2 = ruta_procesado & CARPETA & "\wgs84\" & v_archivo & ".dbf"
        ''        End If

        ''        System.IO.File.Copy(ruta1, ruta2, True)


        ''        ruta1 = glo_pathTMP & "\" & v_archivo_rese & ".prj"
        ''        If v_sistema = "PSAD-56" Then
        ''            ruta2 = ruta_procesado & CARPETA & "\" & v_archivo & ".prj"
        ''        Else
        ''            ruta2 = ruta_procesado & CARPETA & "\wgs84\" & v_archivo & ".prj"
        ''        End If

        ''        System.IO.File.Copy(ruta1, ruta2, True)


        ''    End If
        ''End If
        ''  ElseIf loBoo_flg = False Then
        ''        MsgBox("Seleccione un elemento de la Lista alcanzada", MsgBoxStyle.Information, "SIGCATMIN")
        '' Exit Sub

        '        End If  ' se puso

        '    Next w




        'MsgBox("Se ha terminado el proceso satisfactoriamente...", MsgBoxStyle.Information, "SIGCATMIN...")
        'Catch ex As Exception
        '    MsgBox(" Error en procedimiento de actualizacion de AreaS Restringidas", MsgBoxStyle.Critical, "Observacion")

        'End Try

    End Sub



    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub btnvermapa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnvermapa.Click

        If tipo_catanominero = "AREA RESERVADA" Then
            If v_sistema = "PSAD-56" Then

                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_18", m_application, "1", False)
                'cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_RES_RESERVADA_GEOPSAD56", m_application, "1", False)
                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_17", m_application, "1", False)
                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_19", m_application, "1", False)
            Else

                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_WGS_17", m_application, "1", False)
                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_WGS_18", m_application, "1", False)
                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ARE_AREA_RESERVADA_WGS_19", m_application, "1", False)

            End If
            ' cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_18W", m_application, "1", False)
            ' cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_17W", m_application, "1", False)
            ' cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_19W", m_application, "1", False)
            'cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_AReservada56, m_application, "1", False)
        ElseIf tipo_catanominero = "ZONA URBANA" Then
            'cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ZON_ZONA_URBANA_18", m_application, "1", False)
            'cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_17", m_application, "1", False)
            'cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ZON_ZONA_URBANA_19", m_application, "1", False)
            'cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZON_ZONA_URBANA_19_1", m_application, "1", False)
            If v_sistema = "PSAD-56" Then
                'cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_18", m_application, "1", False)
                'cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_17", m_application, "1", False)
                'cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_19", m_application, "1", False)

                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ZON_ZONA_URBANA_18", m_application, "1", False)
                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ZUR_ZONA_URBANA_17", m_application, "1", False)
                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & ".GPO_ZON_ZONA_URBANA_19", m_application, "1", False)

                ' cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_ZUrbana56, m_application, "1", False)
            Else
                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & "GPO_ZUR_ZONA_URBANA_WGS_18", m_application, "1", False)
                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & "GPO_ZUR_ZONA_URBANA_WGS_18", m_application, "1", False)
                cls_Catastro.PT_CargarFeatureClass_SDE(glo_User_SDE & "GPO_ZUR_ZONA_URBANA_WGS_18", m_application, "1", False)

            End If
        End If

        Me.Close()

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()

    End Sub

   
    Private Sub btn_exporta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_exporta.Click
        Dim cls_eval As New Cls_evaluacion
        Dim ruta1 As String
        Dim ruta2 As String
        If tipo_catanominero = "AREA RESERVADA" Then
            ' Exportando las capas de la geodatabase a Caram Procesado
            For z As Integer = 17 To 19
                If v_sistema = "PSAD-56" Then
                    cls_Catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_" & z, m_application, "1", False)
                Else
                    cls_Catastro.PT_CargarFeatureClass_SDE("GPO_ARE_AREA_RESERVADA_WGS_" & z, m_application, "1", False)

                End If

                'cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_" & z & "W", m_application, "1", False)
                '"CQUI0543.GPO_ARE_AREA_RESERVADA_WGS_18W"
                ' arch_cata = "todo_Rese_18"
                arch_cata = "todo_Rese"
                loStrShapefile_reset = "Rese_" & z & "_" & DateTime.Now.Ticks.ToString
                cls_eval.consultacapaDM(" ", "X_reservas", "X_reservas")
                ' Exit Sub

                cls_Catastro.Exportando_Temas("Zona Reservada", "Zona Reservada", pApp)

                ruta1 = glo_pathTMP & "\" & loStrShapefile_reset & ".shp"
                If v_sistema = "PSAD-56" Then
                    ruta2 = ruta_procesado & "\rese" & z & ".shp"

                Else
                    ruta2 = ruta_procesado & "wgs84\" & "rese" & z & ".shp"
                End If


                System.IO.File.Copy(ruta1, ruta2, True)
                ruta1 = glo_pathTMP & "\" & loStrShapefile_reset & ".shx"
                If v_sistema = "PSAD-56" Then

                    ruta2 = ruta_procesado & "wgs84\" & "rese" & z & ".shx"
                Else
                End If

                System.IO.File.Copy(ruta1, ruta2, True)

                ruta1 = glo_pathTMP & "\" & loStrShapefile_reset & ".dbf"
                If v_sistema = "PSAD-56" Then
                    ruta2 = ruta_procesado & "\rese" & z & ".dbf"

                Else
                    ruta2 = ruta_procesado & "wgs84\" & "rese" & z & ".dbf"
                End If

                System.IO.File.Copy(ruta1, ruta2, True)

                ruta1 = glo_pathTMP & "\" & loStrShapefile_reset & ".prj"
                If v_sistema = "PSAD-56" Then
                    ruta2 = ruta_procesado & "\rese" & z & ".prj"
                Else
                    ruta2 = ruta_procesado & "wgs84\" & "rese" & z & ".prj"
                End If


                System.IO.File.Copy(ruta1, ruta2, True)


            Next z
        ElseIf tipo_catanominero = "ZONA URBANA" Then

            'cls_Catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_18", m_application, "1", False)
            'cls_Catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_17", m_application, "1", False)
            'cls_Catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_19", m_application, "1", False)
            ' cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_ZUrbana56, m_application, "1", False)

            For z As Integer = 17 To 19
                If v_sistema = "PSAD-56" Then
                    cls_Catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_" & z, m_application, "1", False)
                    'cls_Catastro.PT_CargarFeatureClass_SDE("CQUI0543.GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)

                Else
                    cls_Catastro.PT_CargarFeatureClass_SDE("GPO_ZUR_ZONA_URBANA_WGS_" & z, m_application, "1", False)
                End If


                ' arch_cata = "todo_Rese_18"
                arch_cata = "todo_Urba"
                loStrShapefile_reset = "Urba" & z & "_" & DateTime.Now.Ticks.ToString
                cls_eval.consultacapaDM(" ", "X_reservas", "X_reservas")
                cls_Catastro.Exportando_Temas("Zona Urbana", "Zona Urbana", pApp)

                ruta1 = glo_pathTMP & "\" & loStrShapefile_reset & ".shp"
                If v_sistema = "PSAD-56" Then
                    ruta2 = ruta_procesado & "\urba" & z & ".shp"

                Else
                    ruta2 = ruta_procesado & "wgs84\" & "urba" & z & ".shp"
                End If


                System.IO.File.Copy(ruta1, ruta2, True)
                ruta1 = glo_pathTMP & "\" & loStrShapefile_reset & ".shx"
                If v_sistema = "PSAD-56" Then
                    ruta2 = ruta_procesado & "\urba" & z & ".shx"
                Else
                    ruta2 = ruta_procesado & "wgs84\" & "urba" & z & ".shx"
                End If

                System.IO.File.Copy(ruta1, ruta2, True)

                ruta1 = glo_pathTMP & "\" & loStrShapefile_reset & ".dbf"
                If v_sistema = "PSAD-56" Then
                    ruta2 = ruta_procesado & "\urba" & z & ".dbf"
                Else
                    ruta2 = ruta_procesado & "wgs84\" & "urba" & z & ".dbf"
                End If

                System.IO.File.Copy(ruta1, ruta2, True)

                ruta1 = glo_pathTMP & "\" & loStrShapefile_reset & ".prj"
                If v_sistema = "PSAD-56" Then
                    ruta2 = ruta_procesado & "\urba" & z & ".prj"
                Else
                    ruta2 = ruta_procesado & "wgs84\" & "urba" & z & ".prj"
                End If

                System.IO.File.Copy(ruta1, ruta2, True)


            Next z

        End If

        MsgBox("Se ha Finalizado de exportar las capas de reservas a la Unidad de Procesado...", MsgBoxStyle.Information, "SIGCATMIN...")

    End Sub
End Class