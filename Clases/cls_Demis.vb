Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports stdole
Public Class cls_Demis
    'Public Sub PC_AddWMSLayer(ByVal loStrURL As String, ByVal p_App As IApplication)
    '    'Dim loStrURL As String
    '    'loStrURL = "http://www2.demis.nl/wms/wms.asp?wms=WorldMap&Demis World Map"
    '    'loStrNombre = ""
    '    '' Create an WMSMapLayer Instance - this will be added to the map later
    '    Dim wmsMapLayer As IWMSGroupLayer
    '    wmsMapLayer = New WMSMapLayer
    '    '' create and configure wms connection name
    '    '' this is used to store the connection properties
    '    Dim connName As IWMSConnectionName
    '    connName = New WMSConnectionName
    '    Dim propSet As IPropertySet
    '    propSet = New PropertySet
    '    propSet.SetProperty("URL", loStrURL)
    '    connName.ConnectionProperties = propSet
    '    '' uses the name information to connect to the service
    '    Dim dataLayer As IDataLayer
    '    dataLayer = wmsMapLayer
    '    dataLayer.Connect(connName)
    '    '' get service description out of the layer
    '    '' the service description contains inforamtion about the wms categories
    '    '' and layers supported by the service
    '    Dim serviceDesc As IWMSServiceDescription
    '    serviceDesc = wmsMapLayer.WMSServiceDescription
    '    '' for each wms layer either add the stand-alone layer or
    '    '' group layer to the WMSMapLayer which will be added to ArcMap
    '    Dim i As Long
    '    For i = 0 To serviceDesc.LayerDescriptionCount - 1
    '        Dim layerDesc As IWMSLayerDescription
    '        layerDesc = serviceDesc.LayerDescription(i)
    '        Dim newLayer As ILayer
    '        If (layerDesc.LayerDescriptionCount = 0) Then
    '            Dim newWMSLayer As IWMSLayer
    '            newWMSLayer = wmsMapLayer.CreateWMSLayer(layerDesc)
    '            newLayer = newWMSLayer
    '        Else
    '            Dim grpLayer As IWMSGroupLayer
    '            grpLayer = wmsMapLayer.CreateWMSGroupLayers(layerDesc)
    '            newLayer = grpLayer
    '        End If
    '        wmsMapLayer.InsertLayer(newLayer, 0)
    '    Next
    '    '' Configure the layer before adding it to the map
    '    Dim layer As ILayer
    '    layer = wmsMapLayer
    '    layer.Name = serviceDesc.WMSTitle
    '    '' add layer to Map
    '    pMxDoc = p_App.Document
    '    pMxDoc.FocusMap.AddLayer(wmsMapLayer)
    '    'refresh
    '    Dim ActiveView As IActiveView
    '    ActiveView = pMxDoc.FocusMap
    '    ActiveView.Refresh()
    'End Sub


End Class
