Imports System
Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Configuration

Public Class ModuleSettings
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    ' Variables Privadas
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    Private _strCadenaConexion As String
    Private _strCadenaConexionSeguridad As String
    Private _strCadenaConexionGIS As String
    Private _strServidor As String
    Private _strEsquema As String
    Private _strEsquemaSeguridad As String
    Private _strEsquemaGIS As String
    Private _strUnidad As String

    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    ' Propiedades
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    <XmlElement()> Public Property CadenaConexion() As String
        Get
            Return _strCadenaConexion
        End Get
        Set(ByVal Value As String)
            _strCadenaConexion = Value
        End Set
    End Property

    <XmlElement()> Public Property CadenaConexionSeguridad() As String
        Get
            Return _strCadenaConexionSeguridad
        End Get
        Set(ByVal Value As String)
            _strCadenaConexionSeguridad = Value
        End Set
    End Property
    <XmlElement()> Public Property CadenaConexionGIS() As String
        Get
            Return _strCadenaConexionGIS
        End Get
        Set(ByVal Value As String)
            _strCadenaConexionGIS = Value
        End Set
    End Property

    <XmlElement()> Public Property Servidor() As String
        Get
            Return _strServidor
        End Get
        Set(ByVal Value As String)
            _strServidor = Value
        End Set
    End Property

    <XmlElement()> Public Property Esquema() As String
        Get
            Return _strEsquema
        End Get
        Set(ByVal Value As String)
            _strEsquema = Value
        End Set
    End Property

    <XmlElement()> Public Property EsquemaSeguridad() As String
        Get
            Return _strEsquemaSeguridad
        End Get
        Set(ByVal Value As String)
            _strEsquemaSeguridad = Value
        End Set
    End Property
    <XmlElement()> Public Property EsquemaGIS() As String
        Get
            Return _strEsquemaGIS
        End Get
        Set(ByVal Value As String)
            _strEsquemaGIS = Value
        End Set
    End Property

    <XmlElement()> Public Property Unidad() As String
        Get
            Return _strUnidad
        End Get
        Set(ByVal Value As String)
            _strUnidad = Value
        End Set
    End Property

End Class
