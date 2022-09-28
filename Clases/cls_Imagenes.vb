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
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.GeoDatabaseUI
Imports stdole
Public Class cls_Imagenes

    Public Function OpenArcSDERasterCatalog(ByVal sServer As String, _
    ByVal sInstance As String, _
    ByVal sDatabase As String, _
    ByVal sUser As String, _
    ByVal sPassword As String, _
    ByVal sRasterCatalogName As String, _
    Optional ByVal sVersion As String = "sde.DEFAULT") As IRasterCatalog
        ' Open an ArcSDE raster Catalog with the given name
        ' sServer, sInstance, sDatabase, sUser, sPassword, sVersion are database connection info
        ' sRasterCatalogName is the name of the raster Catalog
        'sFile is the filename 
        Dim pWsFact As IWorkspaceFactory
        Dim pWs As IRasterWorkspaceEx
        Dim pRasterCatalog As IRasterCatalog
        Dim pPropertySet As IPropertySet

        'Open the ArcSDE workspace 
        pPropertySet = New PropertySet
        With pPropertySet
            .SetProperty("server", sServer)
            .SetProperty("instance", sInstance)
            .SetProperty("database", sDatabase)
            .SetProperty("user", sUser)
            .SetProperty("password", sPassword)
            .SetProperty("version", sVersion)
        End With
        pWsFact = New SdeWorkspaceFactory
        pWs = pWsFact.Open(pPropertySet, 0)

        'Open the ArcSDE raster Catalog 
        pRasterCatalog = pWs.OpenRasterCatalog(sRasterCatalogName)
        'Return 
        OpenArcSDERasterCatalog = pRasterCatalog
        pWsFact = Nothing
        pWs = Nothing
        pRasterCatalog = Nothing
    End Function

End Class
