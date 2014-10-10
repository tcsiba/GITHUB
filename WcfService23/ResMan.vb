Module ResMan

    Public Function SetResMan(ByVal dataset As String) As Dictionary(Of String, Object)
        Dim rsMan As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
        Dim i As Integer = 0
        Dim context As New NativeEntities
        Dim query = From it In context.AVCODETABLE
                Where it.DATAAREAID = dataset And it.AVCODETYPE = 1102
                Select it
                Order By it.AVDESCRIPTION

        Dim code As AVCODETABLE
        For Each code In query
            rsMan.Add(code.AVCODEID, code.AVDESCRIPTION)
        Next
        Return rsMan
    End Function
End Module
