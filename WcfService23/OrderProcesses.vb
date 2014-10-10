Imports System
Imports System.Reflection
Imports Microsoft.VisualBasic
Imports System.Data.Objects
Imports System.Data.EntityClient
Imports System.Data
Imports System.Data.Linq


Imports System.IO
Imports System.Configuration
Imports System.ServiceModel.Web
Imports System.Web.Script.Serialization

Imports System.Text
Imports System.Xml

Imports System.Data.OleDb

Imports System.Runtime.InteropServices



Module OrderUtillities


    Private _fields As Object

   
    Function InitConst(ByRef order As Object, ByRef resman As Dictionary(Of String, Object)) As Object

        Dim OrderType As Type = order.GetType
        Dim s As String
        Dim n As String

        Dim j As Integer
        Dim fields As PropertyInfo() = OrderType.GetProperties

        For j = 0 To fields.Length - 1

            Dim pi As PropertyInfo = OrderType.GetProperty(fields(j).Name)
            n = OrderType.Name + "." + fields(j).Name
            If Not pi Is Nothing Then
                s = pi.PropertyType.Name
                If resman.ContainsKey(n) Then
                    If pi.PropertyType.Name = "String" Then
                        Dim pval As String = resman.Item(n).ToString
                        pi.SetValue(order, pval, Nothing)
                    ElseIf pi.PropertyType.Name = "Decimal" Then
                        'Dim pval As Decimal = Decimal (resman.Item(s).ToString)
                        Dim pval As Decimal = Convert.ToDecimal(resman.Item(n))
                        pi.SetValue(order, pval, Nothing)
                    ElseIf pi.PropertyType.Name = "Int32" Then
                        'Dim pval As Decimal = Decimal (resman.Item(s).ToString)
                        Dim pval As Int32 = Convert.ToInt32(resman.Item(n))
                        pi.SetValue(order, pval, Nothing)
                    ElseIf pi.PropertyType.Name = "DateTime" Then
                        'Dim pval As Decimal = Decimal (resman.Item(s).ToString)
                        If resman.Item(n).ToString.Equals("NOW") Then
                            Dim pval As DateTime = DateTime.Now
                            pi.SetValue(order, pval, Nothing)
                        ElseIf resman.Item(n).ToString.Equals("TODAY") Then
                            Dim pval As DateTime = DateTime.Today
                            pi.SetValue(order, pval, Nothing)
                        Else
                            Dim pval As DateTime = DateTime.Parse(resman.Item(n).ToString)
                            pi.SetValue(order, pval, Nothing)
                        End If
                    End If
                End If
            End If
        Next j

        Return order
    End Function
    Function InitAttribute(ByRef order As Object, ByRef atrbt As String, ByRef preskey As String, ByRef resman As Dictionary(Of String, Object)) As Object

        Dim OrderType As Type = order.GetType
        Dim s As String
        Dim n As String

        Dim j As Integer
        Dim fields As PropertyInfo() = OrderType.GetProperties

        If Not OrderType.GetProperties Is Nothing Then
            If Not OrderType.GetProperty(atrbt) Is Nothing Then
                Dim pi As PropertyInfo = OrderType.GetProperty(atrbt)
                n = preskey
                If Not pi Is Nothing Then
                    s = pi.PropertyType.Name
                    If resman.ContainsKey(n) Then
                        If pi.PropertyType.Name = "String" Then
                            Dim pval As String = resman.Item(n).ToString
                            pi.SetValue(order, pval, Nothing)
                        ElseIf pi.PropertyType.Name = "Decimal" Then
                            'Dim pval As Decimal = Decimal (resman.Item(s).ToString)
                            Dim pval As Decimal = Convert.ToDecimal(resman.Item(n))
                            pi.SetValue(order, pval, Nothing)
                        ElseIf pi.PropertyType.Name = "Int32" Then
                            'Dim pval As Decimal = Decimal (resman.Item(s).ToString)
                            Dim pval As Int32 = Convert.ToInt32(resman.Item(n))
                            pi.SetValue(order, pval, Nothing)
                        ElseIf pi.PropertyType.Name = "DateTime" Then
                            'Dim pval As Decimal = Decimal (resman.Item(s).ToString)
                            If resman.Item(n).ToString.Equals("NOW") Then
                                Dim pval As DateTime = DateTime.Now
                                pi.SetValue(order, pval, Nothing)
                            ElseIf resman.Item(n).ToString.Equals("TODAY") Then
                                Dim pval As DateTime = DateTime.Today
                                pi.SetValue(order, pval, Nothing)
                            Else
                                Dim pval As DateTime = DateTime.Parse(resman.Item(n).ToString)
                                pi.SetValue(order, pval, Nothing)
                            End If
                        End If
                    End If
                End If
            End If
        End If



        Return order
    End Function

    Public Function InitOrigInsteadOfNull(ByRef order As Object, ByRef origorder As Object) As Object


        Dim OrderType As Type = order.GetType
        Dim s As String

        Dim j As Integer
        Dim fields As PropertyInfo() = OrderType.GetProperties

        Dim ss As Object = Chr(2)
        Dim dd As New Date(1, 1, 1)
        Dim dd0 As New DateTime(1900, 1, 1, 0, 0, 0)

        For j = 0 To fields.Length - 1

            Dim pi As PropertyInfo = OrderType.GetProperty(fields(j).Name)
            If Not pi Is Nothing Then
                s = pi.PropertyType.Name
                If pi.PropertyType.Name = "String" Then
                    If pi.GetValue(order, Nothing) Is Nothing Then
                        Dim xval As String = pi.GetValue(origorder, Nothing).ToString
                        pi.SetValue(order, xval, Nothing)
                    End If
                End If
                If pi.PropertyType.Name = "DateTime" Then
                    If pi.GetValue(order, Nothing) < dd0 Then
                        Dim xval As DateTime = pi.GetValue(origorder, Nothing)
                        pi.SetValue(order, xval, Nothing)
                    End If
                End If
                If pi.PropertyType.Name = "Decimal" Then
                    If Not pi.GetValue(order, Nothing) = 0 Then
                        Dim xval As Decimal = pi.GetValue(origorder, Nothing)
                        pi.SetValue(order, xval, Nothing)
                    End If
                End If
                If pi.PropertyType.Name = "Int32" Then
                    If Not pi.GetValue(order, Nothing) = 0 Then
                        Dim xval As Int32 = pi.GetValue(origorder, Nothing)
                        pi.SetValue(order, xval, Nothing)
                    End If
                End If

            End If

        Next j

        Return order
    End Function

    

    Public Function InitDefault(ByRef order As Object) As Object


        Dim OrderType As Type = order.GetType
        Dim s As String

        Dim j As Integer
        Dim fields As PropertyInfo() = OrderType.GetProperties

        Dim ss As Object = Chr(2)
        Dim dd As New Date(1, 1, 1)
        Dim dd0 As New DateTime(1900, 1, 1, 0, 0, 0)

        For j = 0 To fields.Length - 1

            Dim pi As PropertyInfo = OrderType.GetProperty(fields(j).Name)
            If Not pi Is Nothing Then
                s = pi.PropertyType.Name
                If pi.PropertyType.Name = "String" Then
                    If pi.GetValue(order, Nothing) Is Nothing Then
                        pi.SetValue(order, ss.ToString, Nothing)
                    End If
                End If
                If pi.PropertyType.Name = "DateTime" Then
                    If pi.GetValue(order, Nothing) < dd0 Then
                        pi.SetValue(order, dd0, Nothing)
                    End If
                End If
            End If

        Next j

        Return order
    End Function

    Public Function setcheckExtOrdernum(ByVal order As AVUNBILLEDORDERS) As AVUNBILLEDORDERS
        If Not order.AVEXTERNALORDERNUM Is Nothing Then
            Dim query = From it In context.AVUNBILLEDORDERS
                Where it.DATAAREAID = order.DATAAREAID And it.AVEXTERNALORDERNUM = order.AVEXTERNALORDERNUM
                Select it
            Dim testorder As AVUNBILLEDORDERS = query.FirstOrDefault
            If Not testorder Is Nothing Then
                order.SALESID = testorder.SALESID
            End If
        End If
        Return order
    End Function

    Public Function setCheckSalesid(ByVal order As AVUNBILLEDORDERS) As AVUNBILLEDORDERS
        If order.SALESID Is Nothing Then
            order.SALESID = GetNextSeq(order.DATAAREAID, "SALESID")
        End If
        Return order
    End Function

    Public Function ValidateItems(ByRef item As AVUNBILLEDORDERLINES, ByRef resp As ORDERRESPONSE) As ORDERRESPONSE

        Dim itemdata As New PRICEDISCTABLE
        Dim origitemdata As New PRICEDISCTABLE
        Dim test As New AVUNBILLEDORDERLINES
        Dim pricedisckey As New EntityKey

        itemdata.DATAAREAID = item.DATAAREAID
        itemdata.AVCAMPAIGNID = item.AVCAMPAIGNOFPRICE
        itemdata.AVBROCHURELINENUM = item.AVBROCHURELINENUM

        pricedisckey = context.CreateEntityKey("PRICEDISCTABLE", itemdata)

        If context.TryGetObjectByKey(pricedisckey, origitemdata) Then
            item.ITEMID = origitemdata.ITEMRELATION
            item.AVCUSTUNITPRICE = origitemdata.AVCUSTUNITPRICE
            item.AVNUMBERFOR = origitemdata.PRICEUNIT
        Else
            item.AVITEMSTATUSCODE = 1
        End If
        If Not (item.AVREPLACEMENTCAMPAIGN Is Nothing) And (item.AVREPLACEMENTLINENUM > 0) Then
            itemdata.DATAAREAID = item.DATAAREAID
            itemdata.AVCAMPAIGNID = item.AVCAMPAIGNOFPRICE
            itemdata.AVBROCHURELINENUM = item.AVBROCHURELINENUM

            pricedisckey = context.CreateEntityKey("PRICEDISCTABLE", itemdata)
            If context.TryGetObjectByKey(pricedisckey, origitemdata) Then
                item.ITEMID = origitemdata.ITEMRELATION
                item.AVCUSTUNITPRICE = origitemdata.AVCUSTUNITPRICE
                item.AVNUMBERFOR = origitemdata.PRICEUNIT

            Else
                item.AVITEMSTATUSCODE = 1
            End If
        End If
        Return resp
    End Function
    Public Function ValidatePendingItems(ByRef item As AVSHORTPENDINGLINES, ByRef resp As SHORTSRESPONSE) As SHORTSRESPONSE

        Dim itemdata As New PRICEDISCTABLE
        Dim origitemdata As New PRICEDISCTABLE
        Dim test As New AVSHORTPENDINGLINES
        Dim pricedisckey As New EntityKey

        itemdata.DATAAREAID = item.DATAAREAID
        itemdata.AVCAMPAIGNID = item.AVCAMPAIGNOFPRICE
        itemdata.AVBROCHURELINENUM = item.AVBROCHURELINENUM

        pricedisckey = context.CreateEntityKey("PRICEDISCTABLE", itemdata)

        If context.TryGetObjectByKey(pricedisckey, origitemdata) Then
            item.ITEMID = origitemdata.ITEMRELATION
            item.AVCUSTUNITPRICE = origitemdata.AVCUSTUNITPRICE
            item.AVNUMBERFOR = origitemdata.PRICEUNIT

        Else
            item.AVITEMSTATUSCODE = 1
        End If
        If Not (item.AVREPLACEMENTCAMPAIGN Is Nothing) And (item.AVREPLACEMENTLINENUM > 0) Then
            itemdata.DATAAREAID = item.DATAAREAID
            itemdata.AVCAMPAIGNID = item.AVCAMPAIGNOFPRICE
            itemdata.AVBROCHURELINENUM = item.AVBROCHURELINENUM

            pricedisckey = context.CreateEntityKey("PRICEDISCTABLE", itemdata)
            If context.TryGetObjectByKey(pricedisckey, origitemdata) Then
                item.ITEMID = origitemdata.ITEMRELATION
                item.AVCUSTUNITPRICE = origitemdata.AVCUSTUNITPRICE
                item.AVNUMBERFOR = origitemdata.PRICEUNIT
                'item.TAXRATE = origitemdata
            Else
                item.AVITEMSTATUSCODE = 1
            End If
        End If

        Return resp
    End Function

    Public Function GetManifestId(ByVal order As AVUNBILLEDORDERS) As Object()

        Dim sSQL As String = "", dt As New DataTable

        Dim myDB As New OleDbConnection
        myDB.ConnectionString = ConfigurationManager.ConnectionStrings("OraConnGI").ConnectionString


        sSQL = "SELECT s.DISPATCHDATE,s.MANIFESTID,s.INVENTLOCATION FROM TABLE(MANIFESTSERVICE(?).GETDATA(?, ?, ?, ?,?,?,?,?,?,?,?,?,?)) s "
        If Not ConnectionState.Open Then
            myDB.Open()
        End If

        Dim command As New OleDbCommand(sSQL, myDB)
        command.Parameters.Add("@DATAAREAID", OleDbType.VarChar, 5)
        command.Parameters.Add("@AVGROUPCODEID", OleDbType.VarChar, 150)
        command.Parameters.Add("@AVCDC", OleDbType.VarChar, 150)
        command.Parameters.Add("@AVORDERSOURCE", OleDbType.VarChar, 150)
        command.Parameters.Add("@ZIPCODE", OleDbType.VarChar, 150)
        command.Parameters.Add("@AVORDERTYPE", OleDbType.VarChar, 150)
        command.Parameters.Add("@AVPRIORITYCODE", OleDbType.VarChar, 150)
        command.Parameters.Add("@AVSHIPIMMEDCODE", OleDbType.VarChar, 150)
        command.Parameters.Add("@AVDELIVERYINSTRUCTIONTYPE", OleDbType.VarChar, 150)
        command.Parameters.Add("@CUSTACCOUNT", OleDbType.VarChar, 150)
        command.Parameters.Add("@AVEXTERNALACCOUNTNUM", OleDbType.VarChar, 150)
        command.Parameters.Add("@AVMAILPLAN", OleDbType.VarChar, 150)
        command.Parameters.Add("@AVCAMPAIGNOFORDER", OleDbType.VarChar, 150)
        command.Parameters.Add("@MODIFIEDDATE", OleDbType.Date, 16)



        command.Parameters("@DATAAREAID").Value = order.DATAAREAID
        command.Parameters("@AVGROUPCODEID").Value = order.AVGROUPCODEID
        command.Parameters("@AVCDC").Value = order.AVCDC
        command.Parameters("@AVORDERSOURCE").Value = order.AVORDERSOURCE
        command.Parameters("@ZIPCODE").Value = order.ZIPCODE
        command.Parameters("@AVORDERTYPE").Value = order.AVORDERTYPE
        command.Parameters("@AVPRIORITYCODE").Value = order.AVPRIORITYCODE
        command.Parameters("@AVSHIPIMMEDCODE").Value = order.AVSHIPIMMEDCODE
        command.Parameters("@AVDELIVERYINSTRUCTIONTYPE").Value = order.AVDELIVERYINSTRUCTIONTYPE
        command.Parameters("@CUSTACCOUNT").Value = order.CUSTACCOUNT
        command.Parameters("@AVEXTERNALACCOUNTNUM").Value = order.AVEXTERNALACCOUNTNUM
        command.Parameters("@AVMAILPLAN").Value = order.AVMAILPLAN
        command.Parameters("@AVCAMPAIGNOFORDER").Value = order.AVCAMPAIGNOFORDER
        command.Parameters("@MODIFIEDDATE").Value = order.MODIFIEDDATE

        command.Prepare()


        Dim reader As OleDbDataReader = command.ExecuteReader()

        Dim out(2) As Object
        While reader.Read()
            out(0) = reader.GetDateTime(0)

            If reader.IsDBNull(1) Then
                out(1) = Chr(2)
            Else
                out(1) = reader.GetString(1)
            End If
            If reader.IsDBNull(1) Then
                out(2) = Chr(2)
            Else
                out(2) = reader.GetString(2)
            End If

        End While
        If ConnectionState.Open Then
            myDB.Close()
        End If


        GetManifestId = out


    End Function
    Public Function GetNextSeq(ByVal dataareaid As String, ByVal seqid As String) As Int32

        Dim sSQL As String = "", dt As New DataTable

        Dim myDB As New OleDbConnection
        myDB.ConnectionString = ConfigurationManager.ConnectionStrings("OraConnNative").ConnectionString


        sSQL = "SELECT UF_GET_GM_SEQNO (?, ?, 1) AS salesid from dual"

        If Not ConnectionState.Open Then
            myDB.Open()
        End If

        Dim command As New OleDbCommand(sSQL, myDB)
        command.Parameters.Add("@DATAAREAID", OleDbType.VarChar, 5)
        command.Parameters.Add("@SEQID", OleDbType.VarChar, 150)



        command.Parameters("@DATAAREAID").Value = dataareaid
        command.Parameters("@SEQID").Value = seqid

        command.Prepare()


        Dim reader As OleDbDataReader = command.ExecuteReader()

        Dim out As Int32
        While reader.Read()
            out = reader.GetValue(0)
        End While
        If ConnectionState.Open Then
            myDB.Close()
        End If
        GetNextSeq = out


    End Function

    Public Function NewOrder(ByRef indata As ORDER, ByRef resMan As Dictionary(Of String, Object)) As ORDERRESPONSE
        'Dim context As New NativeEntities

        Dim order As New AVUNBILLEDORDERS
        Dim itemdata As New PRICEDISCTABLE
        Dim originalorder As New AVUNBILLEDORDERS
        Dim originalitem As New AVUNBILLEDORDERLINES

        Dim orderkey As EntityKey
        Dim itemkey As EntityKey

        'Dim test As New RFRSH_LOG
        Dim resp As New ORDERRESPONSE
        Dim a(0) As String
        Dim b(0) As Integer
        resp.RESULT = b
        resp.RESULTTXT = a
        Dim cd(1) As Integer
        Dim txt(1) As String

        order = indata.order
        'allocate dispatchdate-manifest id
        Dim obj() As Object = GetManifestId(order)

        order.AVDELIVERYDATE = Date.Parse(obj(0))
        order.AVSHIPMENTID = obj(1).ToString
        order.AVCDC = obj(2).ToString

        ' check and set if order exists and update salesid by externalordernum if it exists
        order = setcheckExtOrdernum(order)
        ' check and set salesid if it is still null

        order = setCheckSalesid(order)
        orderkey = context.CreateEntityKey("AVUNBILLEDORDERS", order)

        Dim ind As Integer
        ind = 0
        Try

            ' Update order with incoming values otherwise  insert
            If context.TryGetObjectByKey(orderkey, originalorder) Then
                InitOrigInsteadOfNull(order, originalorder)
                initRecord(order, 0)
                context.ApplyCurrentValues(orderkey.EntitySetName, order)
            Else
                ' Init market specific defaults
                order = InitConst(order, resMan)
                ' Incoming object is updatef if null values are delivered
                order = InitDefault(order)
                initRecord(order, 1)
                context.AddToAVUNBILLEDORDERS(order)
            End If

            For Each testx As AVUNBILLEDORDERLINES In indata.items
                ind = ind + 1
                testx.CUSTACCOUNT = order.CUSTACCOUNT
                testx.DATAAREAID = order.DATAAREAID
                testx.SALESID = order.SALESID
                If testx.SEQNUM = 0 Then testx.SEQNUM = ind
                If testx.RECID = 0 Then testx.RECID = ind
                itemkey = context.CreateEntityKey("AVUNBILLEDORDERLINES", testx)

                resp = ValidateItems(testx, resp)
                If context.TryGetObjectByKey(itemkey, originalitem) Then
                    InitOrigInsteadOfNull(testx, originalitem)
                    initRecord(testx, 0)
                    context.ApplyCurrentValues(itemkey.EntitySetName, testx)
                Else
                    testx = setItemSetup("I", testx)  'setup calculated fields
                    testx = InitConst(testx, resMan)
                    testx = InitDefault(testx)
                    initRecord(testx, 1)
                    ' Update order with incoming values otherwise  insert
                    context.AddToAVUNBILLEDORDERLINES(testx)
                End If
            Next
            context.SaveChanges()
            cd(0) = 0
            resp.RESULT = cd
            txt(0) = " order:" + order.SALESID + " succesfully loaded of " + indata.items.Length.ToString + "  items"
            resp.RESULTTXT = txt
        Catch ex As Exception
            cd(0) = -99
            resp.RESULT = cd
            txt(0) = " bean error at order:" + order.SALESID + " of " + indata.items.Length.ToString + "  items error details>>>" + ex.ToString
            resp.RESULTTXT = txt
        End Try
        Return resp
    End Function

    Public Function AddShortPending(ByRef indata As SHORTS, ByRef resMan As Dictionary(Of String, Object)) As SHORTSRESPONSE
        'Dim context As New NativeEntities
        Dim testx As New AVSHORTPENDINGLINES
        Dim item As New AVSHORTPENDINGLINES_ext
        Dim baseitem As New AVSHORTPENDINGLINES
        Dim itemdata As New PRICEDISCTABLE
        Dim originalitem As New AVSHORTPENDINGLINES

        Dim itemkey As EntityKey

        'Dim test As New RFRSH_LOG
        Dim resp As New SHORTSRESPONSE
        Dim a(0) As String
        Dim b(0) As Integer
        resp.RESULT = b
        resp.RESULTTXT = a
        Dim cd(1) As Integer
        Dim txt(1) As String

        Dim ind As Integer = 0

        Try
            For Each testy As AVSHORTPENDINGLINES_ext In indata.items
                ind = ind + 1

                testx = testy.getBase

                If testx.RECID = 0 Then
                    testx.RECID = GetNextSeq(testx.DATAAREAID, "GMID")
                End If
                If testx.SEQNUM = 0 Then
                    testx.SEQNUM = testx.RECID
                End If

                testx.CUSTACCOUNT = testy.CUSTACCOUNT
                    testx.DATAAREAID = testy.DATAAREAID

                    itemkey = context.CreateEntityKey("AVSHORTPENDINGLINES", testx)

                    resp = ValidatePendingItems(testx, resp)
                    If context.TryGetObjectByKey(itemkey, originalitem) Then
                        If Not testy.ACTION.Equals("D") Then
                        InitOrigInsteadOfNull(testx, originalitem)
                        initRecord(testx, 0)
                            context.ApplyCurrentValues(itemkey.EntitySetName, testx)
                        Else
                            context.DeleteObject(originalitem)
                        End If
                    Else
                    If Not testy.ACTION.Equals("D") Then
                        initRecord(testx, 1)
                        testx = setItemSetup("I", testx)
                        testx = InitConst(testx, resMan)
                        testx = InitDefault(testx)
                        ' Update order with incoming values otherwise  insert
                        context.AddToAVSHORTPENDINGLINES(testx)
                    End If
                    End If
            Next
            context.SaveChanges()
            cd(0) = 0
            resp.RESULT = cd
            txt(0) = " pending items for rep:" + indata.items(0).CUSTACCOUNT + " succesfully loaded of " + indata.items.Length.ToString + "  items"
            resp.RESULTTXT = txt
        Catch ex As Exception
            cd(0) = -99
            resp.RESULT = cd
            txt(0) = " bean error at pending item submit rep:" + indata.items(0).CUSTACCOUNT + " of " + indata.items.Length.ToString + "  items error details>>>" + ex.ToString
            resp.RESULTTXT = txt
        End Try
        Return resp
    End Function

End Module
