Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization


Imports System.Xml.XmlNamespaceManager
Imports System.Xml.Schema



Public Class ORDER

    <XmlElement(ElementName:="AVUNBILLEDORDERS")> _
    Public order As AVUNBILLEDORDERS
    <XmlElement(ElementName:="AVUNBILLEDORDERLINES")> _
    Public items() As AVUNBILLEDORDERLINES
End Class


Public Class SHORTS
    <XmlElement(ElementName:="AVSHORTPENDINGLINES")> _
    Public items() As AVSHORTPENDINGLINES_ext
End Class



