Imports System.Xml.Schema
Imports System.Reflection

Public Class AVUNBILLEDORDERS_ext
    Inherits AVUNBILLEDORDERS

    Public ACTION As String

End Class


Public Class AVUNBILLEDORDERLINES_ext
    Inherits AVUNBILLEDORDERLINES

    Public ACTION As String

End Class

Public Class AVSHORTPENDINGLINES_ext
    Inherits AVSHORTPENDINGLINES

    Public ACTION As String
    Public Function getBase() As AVSHORTPENDINGLINES
        Dim a As New AVSHORTPENDINGLINES

        
        Return getSuper(a, Me)
       
    End Function
End Class


Public Class AVINCENTIVELOG_ext
    Inherits AVINCENTIVELOG

    Public ACTION As String

    Public Function getBase() As AVINCENTIVELOG
        Dim a As AVINCENTIVELOG = MyBase.MemberwiseClone()
        Return a
    End Function
End Class
