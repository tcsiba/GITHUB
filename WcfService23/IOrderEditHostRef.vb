' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
<ServiceContract()>
Public Interface IOrderEditHostRef

    <OperationContract()>
    Function NewOrderXML(ByVal input As String) As String

    <OperationContract()>
    Function AddEditShortXML(ByVal input As String) As String

    ' TODO: Add your service operations here

End Interface

' Use a data contract as illustrated in the sample below to add composite types to service operations.

<DataContract()>
Public Class CompositeType

    <DataMember()>
    Public Property BoolValue() As Boolean

    <DataMember()>
    Public Property StringValue() As String

End Class
