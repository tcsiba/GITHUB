Imports System.Reflection

Public Module Common
    Public context As New NativeEntities
    Public rsMan As Dictionary(Of String, Object) = New Dictionary(Of String, Object)

    Public Sub initRecord(ByRef input As Object, ByRef opt As Integer)
        If opt = 1 Then
            input.CREATEDDATE = Now
            input.CREATEDBY = "WFC-ORDWREDIT-" '+ context.Connection.GetSchema.ToString

        End If
        input.MODIFIEDDATE = Now
        input.MODIFIEDBY = "WFC-ORDWREDIT-" '+ context.Connection.GetSchema.ToString

    End Sub

    Public Function setItemSetup(ByRef action As String, ByRef testy As Object) As Object
        If action = "I" And testy.QTYORDERED > 0 And testy.AVREPLACEMENTCAMPAIGN Is Nothing Then ' normal purchase
            If testy.AVITEMSOURCE Is Nothing Then testy.AVITEMSOURCE = "01"
            If testy.AVITEMSOURCEORIGINAL Is Nothing Then testy.AVITEMSOURCEORIGINALINAL = "01"
        End If
        If action = "I" And testy.QTYORDERED > 0 And Not testy.AVREPLACEMENTCAMPAIGN Is Nothing Then ' free exchange
            If testy.AVITEMSOURCE Is Nothing Then testy.AVITEMSOURCE = "03"
            If testy.AVITEMSOURCEORIGINAL Is Nothing Then testy.AVITEMSOURCEORIGINALINAL = "03"
        End If

        If action = "I" And testy.QTYORDERED < 0 And testy.AVRETURNSFORMDOCNUM = 0 Then ' credit with no PH 
            If testy.AVITEMSOURCE Is Nothing Then testy.AVITEMSOURCE = "96"
            If testy.AVITEMSOURCEORIGINAL Is Nothing Then testy.AVITEMSOURCEORIGINALINAL = "96"
        End If
        If action = "I" And testy.QTYORDERED < 0 And testy.AVRETURNSFORMDOCNUM > 0 Then ' credit with  PH 
            If testy.AVITEMSOURCE Is Nothing Then testy.AVITEMSOURCE = "36"
            If testy.AVITEMSOURCEORIGINAL Is Nothing Then testy.AVITEMSOURCEORIGINALINAL = "36"
        End If
        Dim ptype As Type = testy.GetType
        Dim fields As PropertyInfo() = ptype.GetProperties
        'Dim pi As PropertyInfo = ptype.GetProperty("AVPENDINGCREDITSTATUSCODE")
        If Not ptype.GetProperty("AVPENDINGCREDITSTATUSCODE") Is Nothing Then
            If action = "I" And testy.QTYORDERED < 0 Then ' credit with  PH 
                testy.AvPendingCreditStatusCode = "1"
            Else
                testy.AvPendingCreditStatusCode = "0"
            End If
        End If
        Return testy
    End Function

    Public Function getSuper(ByVal a As Object, ByVal b As Object) As Object
        Dim ptype As Type = a.GetType
        Dim btype As Type = b.GetType
        Dim s As String
        Dim dd0 As New DateTime(1900, 1, 1, 0, 0, 0)
        Dim fields As PropertyInfo() = ptype.GetProperties
        For j = 0 To fields.Length - 1
            Dim pi As PropertyInfo = ptype.GetProperty(fields(j).Name)
            Dim pii As PropertyInfo = btype.GetProperty(fields(j).Name)
            If Not pi Is Nothing Then
                s = pi.PropertyType.Name
                If pi.PropertyType.Name = "String" Then
                    If Not pii.GetValue(b, Nothing) Is Nothing Then
                        pi.SetValue(a, pii.GetValue(b, Nothing), Nothing)
                    End If
                End If
                If pi.PropertyType.Name = "DateTime" Then
                    If pii.GetValue(b, Nothing) > dd0 Then
                        pi.SetValue(a, pii.GetValue(b, Nothing), Nothing)
                    End If
                End If
                If pi.PropertyType.Name = "Decimal" Then
                    If Not pii.GetValue(b, Nothing) = 0 Then
                        pi.SetValue(a, pii.GetValue(b, Nothing), Nothing)
                    End If
                End If
                If pi.PropertyType.Name = "Int32" Then
                    If Not pii.GetValue(b, Nothing) = 0 Then
                        pi.SetValue(a, pii.GetValue(b, Nothing), Nothing)
                    End If
                End If
            End If
        Next
        Return a
    End Function

End Module
