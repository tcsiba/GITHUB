' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in code, svc and config file together.
Imports System.Xml

Imports System.IO.StringReader
Imports System.IO.MemoryStream
Imports System.Reflection
Imports System.Data.Objects
Imports System.Xml.Serialization


Public Class Service1
    Implements IOrderEditHostRef

    Public Sub New()
    End Sub

    Public Function NewOrderXML(ByVal input As String) As String Implements IOrderEditHostRef.NewOrderXML
        'YYYY-MM-DD 
        'YYYY-MM-DDTHH:MI:SS

        Dim indata As New ORDER
        Dim resp As New ORDERRESPONSE
        Dim mySerializer As XmlSerializer = New XmlSerializer(GetType(ORDER))
        Dim responseSerializer As XmlSerializer = New XmlSerializer(GetType(ORDERRESPONSE))

        Dim cd(1) As Integer
        Try
            Dim mystringreader As New IO.StringReader(input)
            'serialize input class from incom,ing string
            indata = mySerializer.Deserialize(mystringreader)

            'get resource manager
            rsMan = SetResMan(indata.order.DATAAREAID)
            'execute bean operation
            resp = NewOrder(indata, rsMan)
            'deserialize response and retive as string
            resp.SALESID = indata.order.SALESID
            resp.DATAAREAID = indata.order.DATAAREAID
            resp.CUSTACCOUNT = indata.order.CUSTACCOUNT
            resp.AVEXTERNALORDERNUM = indata.order.AVEXTERNALORDERNUM


        Catch ex As Exception
            'Dim cd(1) As Integer

            cd(0) = -999
            resp.RESULT = cd
            Dim txt(1) As String
            txt(0) = " exception:" + ex.ToString
            resp.RESULTTXT = txt
        End Try
        Dim sw As New IO.StringWriter()
        responseSerializer.Serialize(sw, resp)

        NewOrderXML = resp.RESULT.ToString


        NewOrderXML = sw.ToString
    End Function
    Public Function AddEditShortXML(ByVal input As String) As String Implements IOrderEditHostRef.AddEditShortXML
        'YYYY-MM-DD 
        'YYYY-MM-DDTHH:MI:SS

        Dim indata As New SHORTS
        Dim resp As New SHORTSRESPONSE
        Dim mySerializer As XmlSerializer = New XmlSerializer(GetType(SHORTS))
        Dim responseSerializer As XmlSerializer = New XmlSerializer(GetType(SHORTSRESPONSE))

        Dim cd(1) As Integer
        Try
            Dim mystringreader As New IO.StringReader(input)
            'serialize input class from incom,ing string
            indata = mySerializer.Deserialize(mystringreader)

            'get resource manager
            rsMan = SetResMan(indata.items(0).DATAAREAID)
            'execute bean operation
            resp = AddShortPending(indata, rsMan)
            'deserialize response and retive as string

            resp.DATAAREAID = indata.items(0).DATAAREAID
            resp.CUSTACCOUNT = indata.items(0).CUSTACCOUNT



        Catch ex As Exception
            'Dim cd(1) As Integer

            cd(0) = -999
            resp.RESULT = cd
            Dim txt(1) As String
            txt(0) = " exception:" + ex.ToString
            resp.RESULTTXT = txt
        End Try
        Dim sw As New IO.StringWriter()
        responseSerializer.Serialize(sw, resp)

        'AddEditShortXML = resp.RESULT.ToString
        AddEditShortXML = sw.ToString

    End Function
    

End Class
