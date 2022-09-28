Imports System.IO
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Public Class cls_Google_Earth
    'Public pApp As IApplication
    Const Quote As String = """"
    Private NombreCatastro As String
    Private lostr_FileGoogle As String = ""

    Public Sub Genera_KML_Google1(ByRef m_application As IApplication, ByVal p_Zona As String)
        NombreCatastro = "Catastro"

        Dim FileKML As System.IO.FileStream = New System.IO.FileStream(glo_pathTMP & "\Google.kml", FileMode.Create)

        Dim KML As StreamWriter = New StreamWriter(FileKML)
        Selecciona_datos(m_application)
        If ListarShapeSelecionados(KML, m_application, p_Zona) Then
            KML.Close()
            FileKML.Close()
            QuitarSeleccion(m_application)
            System.Diagnostics.Process.Start(glo_pathTMP & "\Google.kml")
        End If


    End Sub

    Public Sub Genera_KML_Google(ByRef m_application As IApplication, ByVal p_Zona As String)
        NombreCatastro = "Catastro"
        Try
            lostr_FileGoogle = glo_pathTMP & "\Google" & DateTime.Now.Ticks.ToString & ".kml"

            Dim FileKML As System.IO.FileStream = New System.IO.FileStream(lostr_FileGoogle, FileMode.Create)
            Dim KML As StreamWriter = New StreamWriter(FileKML)
            Selecciona_cata_Pantalla(m_application)

            ' If ListarShapeSelecionados(KML, m_application, p_Zona) Then
            ListarShapeSelecionados(KML, m_application, p_Zona)
                KML.Close()
                FileKML.Close()
                QuitarSeleccion(m_application)
                System.Diagnostics.Process.Start(lostr_FileGoogle)
            ' End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Selecciona_cata_Pantalla(ByRef m_application As IApplication)
        pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
        Dim rubberEnv As IRubberBand = New RubberEnvelopeClass()
        Dim geom As IGeometry
        Dim tempEnv As IEnvelope = New EnvelopeClass()
        Dim DisplayTransformation As IDisplayTransformation
        Dim SelectRect As tagRECT
        DisplayTransformation = pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation
        SelectRect.left = 0
        SelectRect.top = 0
        SelectRect.right = DisplayTransformation.DeviceFrame.right
        SelectRect.bottom = DisplayTransformation.DeviceFrame.bottom
        Dim dispTrans As IDisplayTransformation = pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation
        dispTrans.TransformRect(tempEnv, SelectRect, 4)
        geom = CType(tempEnv, IGeometry)
        Dim spatialReference As ISpatialReference = pMap.SpatialReference
        geom.SpatialReference = spatialReference
        pMap.SelectByShape(geom, Nothing, False)
        pMxDoc.ActivatedView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, Nothing, pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation.FittedBounds) ' pMxDoc.ActivatedView.Extent)
    End Sub

    Private Sub Selecciona_datos(ByRef m_application As IApplication)
        pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
        Dim rubberEnv As IRubberBand = New RubberEnvelopeClass()
        Dim geom As IGeometry
        Dim tempEnv As IEnvelope = New EnvelopeClass()
        Dim DisplayTransformation As IDisplayTransformation
        Dim SelectRect As tagRECT
        DisplayTransformation = pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation
        SelectRect.left = 0
        SelectRect.top = 0
        SelectRect.right = DisplayTransformation.DeviceFrame.right
        SelectRect.bottom = DisplayTransformation.DeviceFrame.bottom
        Dim dispTrans As IDisplayTransformation = pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation
        dispTrans.TransformRect(tempEnv, SelectRect, 4)
        geom = CType(tempEnv, IGeometry)
        Dim spatialReference As ISpatialReference = pMap.SpatialReference
        geom.SpatialReference = spatialReference
        pMap.SelectByShape(geom, Nothing, False)
        pMxDoc.ActivatedView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, Nothing, pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation.FittedBounds) ' pMxDoc.ActivatedView.Extent)
    End Sub

    Private Sub QuitarSeleccion(ByRef m_application As IApplication)
        Dim pActiveView As IActiveView
        pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        pMap.ClearSelection()
        pActiveView = pMap
        Dim pBounds As IEnvelope
        pBounds = pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation.FittedBounds
        pMxDoc.ActivatedView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, pBounds)
    End Sub
    Private Function PSAD56WGS84(ByRef pointX As Integer, ByRef pointY As Integer)
        Dim WGS84Point As IPoint = New Point
        Dim SpatRefFact As ISpatialReferenceFactory
        Dim PSAD56_UTM As ISpatialReference
        Dim G_WGS84 As ISpatialReference
        Dim TestStr As String
        SpatRefFact = New SpatialReferenceEnvironment()
        PSAD56_UTM = SpatRefFact.CreateProjectedCoordinateSystem(24877) '4248
        G_WGS84 = SpatRefFact.CreateGeographicCoordinateSystem(4326)
        WGS84Point.PutCoords(pointX, pointY)
        WGS84Point.SpatialReference = PSAD56_UTM
        WGS84Point.Project(G_WGS84)
        TestStr = Format(WGS84Point.X, "###0.00000") + "," + Format(WGS84Point.Y, "###0.00000") + "  " + Format(pointX, "###0.00000") + "," + Format(pointY, "###0.00000")
        MsgBox(TestStr)
        PSAD56WGS84 = WGS84Point
    End Function

    Private Sub WritePolygonCenter(ByRef KML As StreamWriter, ByRef aRec As IFeature, ByRef PSAD56_UTM As ISpatialReference, ByRef G_WGS84 As ISpatialReference)
        Dim WGS84Point As IPoint = New Point
        Dim FormatShape As String
        Dim area As IArea = aRec.Shape
        WGS84Point.PutCoords(area.Centroid.X, area.Centroid.Y)
        WGS84Point.SpatialReference = PSAD56_UTM
        WGS84Point.Project(G_WGS84)
        FormatShape = Format(WGS84Point.X, "###0.00000000") + "," + Format(WGS84Point.Y, "###0.00000000")
        KML.Write(FormatShape + vbNewLine)
    End Sub

    Private Sub WritePolygonPolyLine(ByRef KML As StreamWriter, ByRef aRec As IFeature, ByRef PSAD56_UTM As ISpatialReference, ByRef G_WGS84 As ISpatialReference)
        Dim WGS84Point As IPoint = New Point
        Dim polyPoints As IPointCollection
        Dim VerticePoint As IPoint = New PointClass()
        Dim FormatShape As String
        polyPoints = CType(aRec.Shape, IPointCollection)
        For i As Integer = 0 To polyPoints.PointCount - 1
            VerticePoint = polyPoints.Point(i)
            WGS84Point.PutCoords(VerticePoint.X, VerticePoint.Y)
            WGS84Point.SpatialReference = PSAD56_UTM
            WGS84Point.Project(G_WGS84)
            FormatShape = Format(WGS84Point.X, "###0.00000000") + "," + Format(WGS84Point.Y, "###0.00000000")
            KML.Write(FormatShape + vbNewLine)
        Next i
    End Sub

    Private Sub WritePoint(ByRef KML As StreamWriter, ByRef aRec As IFeature, ByRef PSAD56_UTM As ISpatialReference, ByRef G_WGS84 As ISpatialReference)
        Dim WGS84Point As IPoint = New Point
        Dim FormatShape As String
        Dim Punto As IPoint = New Point
        Punto = CType(aRec.Shape, IPoint)

        WGS84Point.PutCoords(Punto.X, Punto.Y)
        WGS84Point.SpatialReference = PSAD56_UTM
        WGS84Point.Project(G_WGS84)
        FormatShape = Format(WGS84Point.X, "###0.00000000") + "," + Format(WGS84Point.Y, "###0.00000000")
        KML.Write(FormatShape + vbNewLine)
    End Sub

    Private Sub KML_DetalleCatastro(ByRef KML As StreamWriter, ByRef p_Layer As IFeatureLayer, ByRef PSAD56_UTM As ISpatialReference, ByRef G_WGS84 As ISpatialReference)
        Dim counter As Integer = 0
        Dim theClave As String = ""
        Dim theTmpName As String = Nothing
        Dim nameFld As String = Nothing
        Dim theName As String = ""
        Dim theIcono As String = ""
        Dim pFS As IFeatureSelection
        pFS = p_Layer
        Dim pFCursor As IFeatureCursor = Nothing
        pFS.SelectionSet.Search(Nothing, False, pFCursor)
        'Dim aRec As IFeature
        pFeature = pFCursor.NextFeature
        nameFld = "CODIGOU"
        KML.Write("  <Folder>" + vbNewLine)
        KML.Write("    <name>" + "Detalle Catastro" + "</name>" + vbNewLine)
        Do Until pFeature Is Nothing
            counter = counter + 1
            If nameFld <> Nothing Then
                theTmpName = pFeature.Value(pFeature.Class.FindField(nameFld))
                theName = theTmpName.Replace("&", "y")
                theClave = ColorCatastro(pFeature)
                Select Case theClave
                    Case "G1"
                        theIcono = "msn_blue-pushpin_copy1"
                    Case "G2"
                        theIcono = "sn_grn-pushpin"
                    Case "G3"
                        theIcono = "sn_red-pushpin"
                    Case "G4"
                        theIcono = "sn_ltblu-pushpin"
                    Case "G5"
                        theIcono = "sn_wht-pushpin"
                End Select
            End If
            KML.WriteLine("     <Placemark>")
            KML.WriteLine("        <name>" + theName + "</name>")
            KML.WriteLine("      		<description>" + "Detalle" + "</description>")
            KML.WriteLine("        <styleUrl>#" + theIcono + "</styleUrl>")
            KML.WriteLine("        <Point>")
            KML.WriteLine("          <coordinates>")
            WritePolygonCenter(KML, pFeature, PSAD56_UTM, G_WGS84)
            KML.WriteLine("          </coordinates>")
            KML.WriteLine("        </Point>")
            KML.WriteLine("     </Placemark>")
            pFeature = pFCursor.NextFeature
        Loop
        KML.Write("  </Folder>" + vbNewLine)
        pFeature = Nothing
        pFS = Nothing
    End Sub
    Private Sub KML_Data(ByRef KML As StreamWriter, ByRef pLayer1 As IFeatureLayer, ByRef PSAD56_UTM As ISpatialReference, ByRef G_WGS84 As ISpatialReference)
        Dim counter As Integer = 0
        Dim TheColorKML As String = "ffff0000"
        Dim theClave As String
        Dim theTmpName As String = Nothing
        Dim nameFld As String = Nothing
        Dim theName As String
        Dim TheCatastro As Integer = 0
        Dim shpClass As Integer
        Dim pFS As IFeatureSelection
        pFS = pLayer1
        Dim pFCursor As IFeatureCursor = Nothing
        pFS.SelectionSet.Search(Nothing, False, pFCursor)
        pFeature = pFCursor.NextFeature
        shpClass = pLayer1.FeatureClass.ShapeType
        nameFld = Nothing
        If pLayer1.Name = NombreCatastro Then
            nameFld = "Concesion"
            TheCatastro = 1
        End If
        KML.Write("  <Folder>" + vbNewLine)
        KML.Write("    <name>" + pLayer1.Name + "</name>" + vbNewLine)
        Do Until pFeature Is Nothing
            counter = counter + 1
            If nameFld <> Nothing Then
                theTmpName = pFeature.Value(pFeature.Class.FindField(nameFld))
                theName = theTmpName.Replace("&", "y")
                Select Case TheCatastro
                    Case 1
                        theClave = ColorCatastro(pFeature)
                        Select Case theClave
                            Case "G1"
                                TheColorKML = "7dff0000"
                            Case "G2"
                                TheColorKML = "7d00dc02"
                            Case "G3"
                                TheColorKML = "7d0000dc"
                            Case "G4"
                                TheColorKML = "7dffdb00"
                            Case "G5"
                                TheColorKML = "7d000000"
                        End Select
                    Case 2
                        TheColorKML = "50143CDC"
                    Case 4
                        TheColorKML = "50143CDC"
                    Case 5
                        TheColorKML = "501400B4"
                    Case Else
                        theName = Format(counter, "0")
                End Select
            End If
            If shpClass = 1 Then 'esriGeometryPoint
                KML.Write("    <Placemark>" + vbNewLine)
                KML.Write("      <name>" + theName + "</name>" + vbNewLine)
                KML.Write("      <styleUrl>#dot</styleUrl>" + vbNewLine)
                KML.Write("      <Point>" + vbNewLine)
                KML.Write("        <coordinates>" + vbNewLine)
                WritePoint(KML, pFeature, PSAD56_UTM, G_WGS84)
                KML.Write("</coordinates>" + vbNewLine)
                KML.Write("      </Point>" + vbNewLine)
                KML.Write("    </Placemark>" + vbNewLine)
            ElseIf shpClass = 3 Or shpClass = 4 Then
                KML.Write("    <Placemark>" + vbNewLine)
                KML.Write("      <name>" + theName + "</name>" + vbNewLine)
                KML.Write("      <styleUrl>#yline</styleUrl>" + vbNewLine)
                KML.Write("      <LineString>" + vbNewLine)
                KML.Write("      <tessellate>1</tessellate>" + vbNewLine)
                KML.Write("        <coordinates>" + vbNewLine)
                WritePolygonPolyLine(KML, pFeature, PSAD56_UTM, G_WGS84)
                KML.Write("        </coordinates>" + vbNewLine)
                KML.Write("      </LineString>" + vbNewLine)
                KML.Write("        <Style>" + vbNewLine)
                KML.Write("		       <LineStyle>" + vbNewLine)
                KML.Write("			         <color>" + TheColorKML + "</color>" + vbNewLine)
                KML.Write("			        <width>2</width>" + vbNewLine)
                KML.Write("		       </LineStyle>" + vbNewLine)
                KML.Write("        </Style>" + vbNewLine)
                KML.Write("    </Placemark>" + vbNewLine)
                If (TheCatastro = 2) Then
                    KML.Write("     <Placemark>" + vbNewLine)
                    KML.Write("        <name>" + theName + "</name>" + vbNewLine)
                    KML.Write("        <styleUrl>#sn_noicon</styleUrl>" + vbNewLine)
                    KML.Write("        <Point>" + vbNewLine)
                    WritePolygonCenter(KML, pFeature, PSAD56_UTM, G_WGS84)
                    KML.Write("        </Point>" + vbNewLine)
                    KML.Write("     </Placemark>" + vbNewLine)
                End If
            End If
            pFeature = pFCursor.NextFeature
        Loop
        KML.Write("  </Folder>" + vbNewLine)
        pFeature = Nothing
        pFS = Nothing
    End Sub

    Public Function ListarShapeSelecionados(ByRef KML As StreamWriter, ByRef m_application As IApplication, ByVal p_Zona As String) As Boolean
        Dim theTmpName As String = Nothing
        Dim TheColorKML As String = "ffff0000"
        Dim TheCatastro As Integer = 0
        Dim WGS84Point As IPoint = New Point
        Dim SpatRefFact As ISpatialReferenceFactory
        Dim PSAD56_UTM As ISpatialReference = Nothing
        Dim G_WGS84 As ISpatialReference = Nothing
        SpatRefFact = New SpatialReferenceEnvironment()
        Select Case p_Zona
            Case 17
                PSAD56_UTM = SpatRefFact.CreateProjectedCoordinateSystem(24877)
            Case 18
                PSAD56_UTM = SpatRefFact.CreateProjectedCoordinateSystem(24878)
            Case 19
                PSAD56_UTM = SpatRefFact.CreateProjectedCoordinateSystem(24879)
        End Select
        G_WGS84 = SpatRefFact.CreateGeographicCoordinateSystem(4326)
        pMxDoc = m_application.Document
        pMap = pMxDoc.FocusMap
        Dim Afound As Boolean
        Dim A As Integer
        For A = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "Catastro" Then
                Afound = True
                ' Return False
                Exit For
            End If
        Next A
        If Not Afound Then
            MsgBox("No Existe Layer Catastro para Enviarlo a Google Earth ")
            ' Return False
            Exit Function


        End If
        'Return True
        KML.Write("<?xml version=" + Quote + "1.0" + Quote + " encoding=" + Quote + "UTF-8" + Quote + "?>" + vbNewLine)
        KML.Write("<kml xmlns=" + Quote + "http://earth.google.com/kml/2.0" + Quote + ">" + vbNewLine)
        KML.Write("<Document>" + vbNewLine)
        KML.Write("  <name>" + pMap.Layer(A).Name + "</name>" + vbNewLine)
        Call KML_Definiciones(KML)
        If pMap.Layer(A).Name = NombreCatastro Then
            KML_DetalleCatastro(KML, pMap.Layer(A), PSAD56_UTM, G_WGS84)
        End If
        KML_Data(KML, pMap.Layer(A), PSAD56_UTM, G_WGS84)
        KML.Write("</Document>" + vbNewLine)
        KML.Write("</kml>" + vbNewLine)
    End Function
    Private Function ColorCatastro(ByRef aRec As IFeature)
        Dim Estado As String = aRec.Value(aRec.Class.FindField("Estado"))
        If Estado = "E" Or Estado = "N" Or Estado = "Q" Or Estado = "T" Then
            Return "G1"
        End If
        If Estado = "P" Then
            Return "G2"
        End If
        If Estado = "D" Then
            Return "G3"
        End If
        If Estado = "B" Then
            Return "G4"
        End If
        Return "G5"
    End Function

    Private Sub KML_Definiciones(ByRef KML As StreamWriter)
        KML.Write("	<Style id=" + Quote + "sn_noicon" + Quote + ">" + vbNewLine)
        KML.Write("		<IconStyle>" + vbNewLine)
        KML.Write("			<Icon>" + vbNewLine)
        KML.Write("			</Icon>" + vbNewLine)
        KML.Write("		</IconStyle>" + vbNewLine)
        KML.Write("	</Style>" + vbNewLine)

        KML.Write("	<Style id=" + Quote + "sh_grn-pushpin" + Quote + ">" + vbNewLine)
        KML.Write("		<IconStyle>" + vbNewLine)
        KML.Write("			<scale>0.7</scale>" + vbNewLine)
        KML.Write("			<Icon>" + vbNewLine)
        KML.Write("				<href>http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png</href>" + vbNewLine)
        KML.Write("			</Icon>" + vbNewLine)
        KML.Write("			<hotSpot x=" + Quote + "20" + Quote + " y=" + Quote + "2" + Quote + " xunits=" + Quote + "pixels" + Quote + " yunits=" + Quote + "pixels" + Quote + "/>" + vbNewLine)
        KML.Write("		</IconStyle>" + vbNewLine)
        KML.Write("		<ListStyle>" + vbNewLine)
        KML.Write("		</ListStyle>" + vbNewLine)
        KML.Write("	</Style>" + vbNewLine)

        KML.Write("	<StyleMap id=" + Quote + "msn_ltblu-pushpin" + Quote + ">" + vbNewLine)
        KML.Write("		<Pair>" + vbNewLine)
        KML.Write("			<key>normal</key>" + vbNewLine)
        KML.Write("			<styleUrl>#sn_ltblu-pushpin</styleUrl>" + vbNewLine)
        KML.Write("		</Pair>" + vbNewLine)
        KML.Write("		<Pair>" + vbNewLine)
        KML.Write("			<key>highlight</key>" + vbNewLine)
        KML.Write("			<styleUrl>#sn_ltblu-pushpin</styleUrl>" + vbNewLine)
        KML.Write("		</Pair>" + vbNewLine)
        KML.Write("	</StyleMap>" + vbNewLine)

        KML.Write("	<StyleMap id=" + Quote + "msn_wht-pushpin" + Quote + ">" + vbNewLine)
        KML.Write("		<Pair>" + vbNewLine)
        KML.Write("			<key>normal</key>" + vbNewLine)
        KML.Write("			<styleUrl>#sn_wht-pushpin</styleUrl>" + vbNewLine)
        KML.Write("		</Pair>" + vbNewLine)
        KML.Write("		<Pair>" + vbNewLine)
        KML.Write("			<key>highlight</key>" + vbNewLine)
        KML.Write("			<styleUrl>#sh_wht-pushpin</styleUrl>" + vbNewLine)
        KML.Write("		</Pair>" + vbNewLine)
        KML.Write("	</StyleMap>" + vbNewLine)

        KML.Write("	<StyleMap id=" + Quote + "msn_blue-pushpin_copy1" + Quote + ">" + vbNewLine)
        KML.Write("		<Pair>" + vbNewLine)
        KML.Write("			<key>normal</key>" + vbNewLine)
        KML.Write("			<styleUrl>#sn_blue-pushpin_copy1</styleUrl>" + vbNewLine)
        KML.Write("		</Pair>" + vbNewLine)
        KML.Write("		<Pair>" + vbNewLine)
        KML.Write("			<key>highlight</key>" + vbNewLine)
        KML.Write("			<styleUrl>#sh_blue-pushpin_copy1</styleUrl>" + vbNewLine)
        KML.Write("		</Pair>" + vbNewLine)
        KML.Write("	</StyleMap>" + vbNewLine)

        KML.Write("	<Style id=" + Quote + "yline" + Quote + ">" + vbNewLine)
        KML.Write("		<LineStyle>" + vbNewLine)
        KML.Write("			<color>ff00ffff</color>" + vbNewLine)
        KML.Write("		</LineStyle>" + vbNewLine)
        KML.Write("	</Style>" + vbNewLine)

        KML.Write("	<Style id=" + Quote + "sh_ltblu-pushpin" + Quote + ">" + vbNewLine)
        KML.Write("		<IconStyle>" + vbNewLine)
        KML.Write("			<scale>0.7</scale>" + vbNewLine)
        KML.Write("			<Icon>" + vbNewLine)
        KML.Write("				<href>http://maps.google.com/mapfiles/kml/pushpin/ltblu-pushpin.png</href>" + vbNewLine)
        KML.Write("			</Icon>" + vbNewLine)
        KML.Write("			<hotSpot x=" + Quote + "20" + Quote + " y=" + Quote + "2" + Quote + " xunits=" + Quote + "pixels" + Quote + " yunits=" + Quote + "pixels" + Quote + "/>" + vbNewLine)
        KML.Write("		</IconStyle>" + vbNewLine)
        KML.Write("		<ListStyle>" + vbNewLine)
        KML.Write("		</ListStyle>" + vbNewLine)
        KML.Write("	</Style>" + vbNewLine)

        KML.Write("	<Style id=" + Quote + "sh_blue-pushpin_copy1" + Quote + ">" + vbNewLine)
        KML.Write("		<IconStyle>" + vbNewLine)
        KML.Write("			<scale>0.7</scale>" + vbNewLine)
        KML.Write("			<Icon>" + vbNewLine)
        KML.Write("				<href>http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png</href>" + vbNewLine)
        KML.Write("			</Icon>" + vbNewLine)
        KML.Write("			<hotSpot x=" + Quote + "20" + Quote + " y=" + Quote + "2" + Quote + " xunits=" + Quote + "pixels" + Quote + " yunits=" + Quote + "pixels" + Quote + "/>" + vbNewLine)
        KML.Write("		</IconStyle>" + vbNewLine)
        KML.Write("		<ListStyle>" + vbNewLine)
        KML.Write("		</ListStyle>" + vbNewLine)
        KML.Write("	</Style>" + vbNewLine)

        KML.Write("	<Style id=" + Quote + "sn_wht-pushpin" + Quote + ">" + vbNewLine)
        KML.Write("		<IconStyle>" + vbNewLine)
        KML.Write("			<scale>0.7</scale>" + vbNewLine)
        KML.Write("			<Icon>" + vbNewLine)
        KML.Write("				<href>http://maps.google.com/mapfiles/kml/pushpin/wht-pushpin.png</href>" + vbNewLine)
        KML.Write("			</Icon>" + vbNewLine)
        KML.Write("			<hotSpot x=" + Quote + "20" + Quote + " y=" + Quote + "2" + Quote + " xunits=" + Quote + "pixels" + Quote + " yunits=" + Quote + "pixels" + Quote + "/>" + vbNewLine)
        KML.Write("		</IconStyle>" + vbNewLine)
        KML.Write("		<ListStyle>" + vbNewLine)
        KML.Write("		</ListStyle>" + vbNewLine)
        KML.Write("	</Style>" + vbNewLine)

        KML.Write("	<Style id=" + Quote + "sn_ltblu-pushpin" + Quote + ">" + vbNewLine)
        KML.Write("		<IconStyle>" + vbNewLine)
        KML.Write("			<scale>0.7</scale>" + vbNewLine)
        KML.Write("			<Icon>" + vbNewLine)
        KML.Write("				<href>http://maps.google.com/mapfiles/kml/pushpin/ltblu-pushpin.png</href>" + vbNewLine)
        KML.Write("			</Icon>" + vbNewLine)
        KML.Write("			<hotSpot x=" + Quote + "20" + Quote + " y=" + Quote + "2" + Quote + " xunits=" + Quote + "pixels" + Quote + " yunits=" + Quote + "pixels" + Quote + "/>" + vbNewLine)
        KML.Write("		</IconStyle>" + vbNewLine)
        KML.Write("		<ListStyle>" + vbNewLine)
        KML.Write("		</ListStyle>" + vbNewLine)
        KML.Write("	</Style>" + vbNewLine)

        KML.Write("	<StyleMap id=" + Quote + "msn_grn-pushpin" + Quote + ">" + vbNewLine)
        KML.Write("		<Pair>" + vbNewLine)
        KML.Write("			<key>normal</key>" + vbNewLine)
        KML.Write("			<styleUrl>#sn_grn-pushpin</styleUrl>" + vbNewLine)
        KML.Write("		</Pair>" + vbNewLine)
        KML.Write("		<Pair>" + vbNewLine)
        KML.Write("			<key>highlight</key>" + vbNewLine)
        KML.Write("			<styleUrl>#sh_grn-pushpin</styleUrl>" + vbNewLine)
        KML.Write("		</Pair>" + vbNewLine)
        KML.Write("	</StyleMap>" + vbNewLine)

        KML.Write("	<Style id=" + Quote + "sn_blue-pushpin_copy1" + Quote + ">" + vbNewLine)
        KML.Write("		<IconStyle>" + vbNewLine)
        KML.Write("			<scale>0.7</scale>" + vbNewLine)
        KML.Write("			<Icon>" + vbNewLine)
        KML.Write("				<href>http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png</href>" + vbNewLine)

        KML.Write("			</Icon>" + vbNewLine)
        KML.Write("			<hotSpot x=" + Quote + "20" + Quote + " y=" + Quote + "2" + Quote + " xunits=" + Quote + "pixels" + Quote + " yunits=" + Quote + "pixels" + Quote + "/>" + vbNewLine)
        KML.Write("		</IconStyle>" + vbNewLine)
        KML.Write("		<ListStyle>" + vbNewLine)
        KML.Write("		</ListStyle>" + vbNewLine)
        KML.Write("	</Style>" + vbNewLine)

        KML.Write("	<Style id=" + Quote + "sn_grn-pushpin" + Quote + ">" + vbNewLine)
        KML.Write("		<IconStyle>" + vbNewLine)
        KML.Write("			<scale>0.7</scale>" + vbNewLine)
        KML.Write("			<Icon>" + vbNewLine)
        KML.Write("				<href>http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png</href>" + vbNewLine)
        KML.Write("			</Icon>" + vbNewLine)
        KML.Write("			<hotSpot x=" + Quote + "20" + Quote + " y=" + Quote + "2" + Quote + " xunits=" + Quote + "pixels" + Quote + " yunits=" + Quote + "pixels" + Quote + "/>" + vbNewLine)
        KML.Write("		</IconStyle>" + vbNewLine)
        KML.Write("		<ListStyle>" + vbNewLine)
        KML.Write("		</ListStyle>" + vbNewLine)
        KML.Write("	</Style>" + vbNewLine)


        KML.Write("	<Style id=" + Quote + "sn_red-pushpin" + Quote + ">" + vbNewLine)
        KML.Write("		<IconStyle>" + vbNewLine)
        KML.Write("			<scale>0.7</scale>" + vbNewLine)
        KML.Write("			<Icon>" + vbNewLine)
        KML.Write("				<href>http://maps.google.com/mapfiles/kml/pushpin/red-pushpin.png</href>" + vbNewLine)
        KML.Write("			</Icon>" + vbNewLine)
        KML.Write("			<hotSpot x=" + Quote + "20" + Quote + " y=" + Quote + "2" + Quote + " xunits=" + Quote + "pixels" + Quote + " yunits=" + Quote + "pixels" + Quote + "/>" + vbNewLine)
        KML.Write("		</IconStyle>" + vbNewLine)
        KML.Write("		<ListStyle>" + vbNewLine)
        KML.Write("		</ListStyle>" + vbNewLine)
        KML.Write("	</Style>" + vbNewLine)
    End Sub
End Class
