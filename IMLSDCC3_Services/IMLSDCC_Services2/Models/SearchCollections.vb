Imports System.Web.Script.Serialization
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Data
Imports System.Linq.Expressions
Imports System.Data.Linq
Imports System.Data.Linq.SqlClient


Public Class SearchCollections
    Public Class listIs
        Public Property cID As Integer
        Public Property hColl As Integer
        Public Sub New(c, h)
            cID = c
            hColl = h
        End Sub
    End Class
    Public Class items
        Public Property uri As String
        Public Property briefuri As String
        Public Property text As String
        Public Property hitsColl As Integer
        Public Sub New(ByVal xuri As String, ByVal buri As String, ByVal xtext As String, ByVal hColl As Integer)
            uri = xuri
            briefuri = buri
            text = xtext
            hitsColl = hColl
        End Sub
    End Class


    Public Property cStartRecord As Integer
    Public Property query As String
    Public Property cMaximumRecords As Integer
    Public Property totalRecords As Integer
    Public Property uriThisPage As String
    Public Property uriPreviousPage As String
    Public Property itemsFound As Integer
    Public Property uriNextPage As String
    Public Property itemList As List(Of items)
    Public Property itemFacets As List(Of FacetsWithRecords.facets)
    Public Property errors As List(Of String)
    Public Property queries As List(Of String)


    Public Sub New(ByVal searchargs1 As String(), ByVal wheretosearch As String, ByVal dates1 As String, ByVal types1 As String, ByVal places1 As String, ByVal collections1 As String, ByVal startRecord1 As Integer, ByVal maximumRecords1 As Integer, ByVal scope1 As String, ByVal sort1 As String, ByVal withFacets As Boolean)
        Dim db = New IMLSDCCDataContext
        Dim db2 = New IMLSDCCIHDataContext
        Dim maxHitLimit As Integer = 2500
        Dim myPhrase As String                      'this will hold cleaned up / prepared search string
        Dim searching As String
        Dim searchq As String
        Dim temperrors As New List(Of String)
        Dim tempqueries As New List(Of String)
        Dim searchID As String = ""
        If (wheretosearch = "subject") Then
            Dim searchargsTemp As String = ""
            searchID = String.Join(",", searchargs1)
            For Each id In searchargs1
                searchargsTemp = (From s In db.Subjects Where s.subjectID = id Select s.subjectNoPunct).First.ToString
            Next
            searchargs1 = searchargsTemp.Split(",")
        End If
        If (wheretosearch = "type") Then
            searchID = String.Join(",", searchargs1)
            Dim searchargsTemp As String = ""
            For Each id In searchargs1
                searchargsTemp = (From t In db.Types Where t.typeID = id Select t.typeNoPunct).First.ToString
            Next
            searchargs1 = searchargsTemp.Split(",")
        End If
        If (wheretosearch = "date") Then
            searchID = String.Join(",", searchargs1)
            Dim searchargsTemp As String = ""
            For Each id In searchargs1
                searchargsTemp = (From d In db.DateBrowses Where d.dateID = id Select d.dateText).First.ToString
            Next
            searchargs1 = searchargsTemp.Split(",")
        End If
        For Each args In searchargs1
            searching = searching & " " & args
            searchq = searchq & wheretosearch & "=" & args & "&"
        Next

        Dim phrase = Trim(searching)
        myPhrase = GetItemService.Utilities.CleanForSQL(searching)
        myPhrase = Trim(myPhrase)


        If startRecord1 Then
            cStartRecord = startRecord1
        Else
            cStartRecord = 1
            startRecord1 = 1
        End If
        'startRecord = startRecord1
        If maximumRecords1 Then
            cMaximumRecords = maximumRecords1
        Else
            cMaximumRecords = 10
            maximumRecords1 = 10
        End If
        'maximumRecords = maximumRecords1
        totalRecords = 0
        If wheretosearch = "subject" Or wheretosearch = "type" Or wheretosearch = "date" Then
            uriThisPage = "/Search/Collections?" & wheretosearch & "=" & searchID & "&cdates=" & dates1 & "&ctypes=" & types1 & "&cplaces=" & places1 & "&collections=" & collections1 & "&cStartRecord=" & startRecord1 & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
            uriNextPage = "/Search/Collections?" & wheretosearch & "=" & searchID & "&cdates=" & dates1 & "&ctypes=" & types1 & "&cplaces=" & places1 & "&collections=" & collections1 & "&cStartRecord=" & cStartRecord + cMaximumRecords & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
            uriPreviousPage = "/Search/Collections?" & wheretosearch & "=" & searchID & "&cdates=" & dates1 & "&ctypes=" & types1 & "&cplaces=" & places1 & "&collections=" & collections1 & "&cStartRecord=" & cStartRecord - cMaximumRecords & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
            'myPhrase = """" & myPhrase & """"
        Else
            uriThisPage = "/Search/Collections?" & searchq & "cdates=" & dates1 & "&ctypes=" & types1 & "&cplaces=" & places1 & "&collections=" & collections1 & "&cStartRecord=" & startRecord1 & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
            uriNextPage = "/Search/Collections?" & searchq & "cdates=" & dates1 & "&ctypes=" & types1 & "&cplaces=" & places1 & "&collections=" & collections1 & "&cStartRecord=" & cStartRecord + cMaximumRecords & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
            uriPreviousPage = "/Search/Collections?" & searchq & "cdates=" & dates1 & "&ctypes=" & types1 & "&cplaces=" & places1 & "&collections=" & collections1 & "&cStartRecord=" & cStartRecord - cMaximumRecords & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1


            If InStr(myPhrase, Chr(34)) > 0 Then
                ' This handles quoted strings better than PreparePhraseForSQL, except allows more duplicate words in final result
                Dim myPhraseList() As String = myPhrase.Split(Chr(34))
                myPhrase = ""
                For spltCnt = 0 To myPhraseList.Count - 1
                    If myPhraseList(spltCnt) <> "" Then
                        If (spltCnt And 1) = 1 Then
                            myPhrase = myPhrase & " NEAR " & Chr(34) & myPhraseList(spltCnt) & Chr(34)
                        Else
                            Dim prepedPhrase = GetItemService.Utilities.PreparePhraseForSQL(myPhraseList(spltCnt), wheretosearch)
                            If prepedPhrase <> "" And prepedPhrase <> "*" Then
                                myPhrase = myPhrase & " NEAR " & prepedPhrase
                            End If
                        End If
                    End If
                Next
                If myPhrase.Length > 6 Then
                    myPhrase = Mid(myPhrase, 7)
                End If
            Else
                myPhrase = GetItemService.Utilities.PreparePhraseForSQL(myPhrase, wheretosearch)
            End If
        End If
        If places1 Is Nothing Then
            places1 = ""
        End If
        If dates1 Is Nothing Then
            dates1 = ""
        End If
        If types1 Is Nothing Then
            types1 = ""
        End If
        If collections1 Is Nothing Then
            collections1 = ""
        End If
        Dim ret As List(Of listIs), retFacets As List(Of FacetsWithRecords.facets), retTemp As IMultipleResults

        If withFacets = True Then
            retTemp = db.GetCollectionsWithFacets(myPhrase, wheretosearch, maxHitLimit, dates1, types1, places1)
            ret = (From rwf In retTemp.GetResult(Of collCTQResult)() Select New listIs(rwf.collID, rwf.hitsInColl)).ToList() 'rwf.collID, rwf.hitsInColl
            retFacets = retTemp.GetResult(Of FacetsWithRecords.facets)().ToList()
            itemFacets = retFacets.ToList()
        Else
            retTemp = db.GetCollectionsWithFacets(myPhrase, wheretosearch, maxHitLimit, dates1, types1, places1)
            ret = (From rwf In retTemp.GetResult(Of collCTQResult)() Select New listIs(rwf.collID, rwf.hitsInColl)).ToList()
            'retItems = (From ct In db2.collCTQ(myPhrase, wheretosearch, maxHitLimit) Select ct.collID).Distinct.ToList()
        End If

        'ret = retItems


        totalRecords = ret.Count
        'If dates1 <> "" Then
        '    Dim datelist As String()
        '    datelist = dates1.Split(",")
        '    Dim daters = (From q In db2.ControlledVocabularies Where datelist.Contains(q.controlledVocabID) Select q.word).ToList
        '    ret = (From r In ret Join rtd In db2.CollectionProperties On r Equals rtd.collectionID Where daters.Contains(rtd.text) Select r).Distinct.ToList()
        'End If
        'If types1 <> "" Then
        '    Dim typelist As String()
        '    typelist = types1.Split(",")
        '    Dim typers = (From q In db2.ControlledVocabularies Where typelist.Contains(q.controlledVocabID) Select q.word).ToList
        '    ret = (From r In ret Join rtd In db2.CollectionProperties On r Equals rtd.collectionID Where typers.Contains(rtd.text) Select r).Distinct.ToList()
        'End If
        'If places1 <> "" Then
        '    Dim placelist As String()
        '    placelist = places1.Split(",")
        '    Dim placers = (From q In db2.ControlledVocabularies Where placelist.Contains(q.controlledVocabID) Select q.word).ToList
        '    ret = (From r In ret Join rtd In db2.CollectionProperties On r Equals rtd.collectionID Where placers.Contains(rtd.text) Select r).Distinct.ToList()
        'End If
        'If collections1 <> "" Then
        '    Dim collist As String()
        '    collist = collections1.Split(",")
        '    ret = (From r In ret Join rtd In db2.Collections On r Equals rtd.collectionID Where collist.Contains(rtd.collectionID) Select r).Distinct.ToList()
        'End If


        If ret.Count = 0 Then
            temperrors.Add("No results found.")
        Else
            ret = ret.Skip(cStartRecord - 1).Take(cMaximumRecords).ToList()
            Dim tempitemList As New List(Of items)
            Dim list As New List(Of String)
            If Not ret Is Nothing Then

                For Each j In ret
                    Dim rs = (From r In db2.CollectionProperties Where r.collectionID = j.cID And r.property = "title_collection" Select r.text)
                    If (rs.Count > 0) Then
                        Dim z = rs.Single
                        Dim tempitem As New items("/Collection/Detail/" & j.cID, "/Collection/Brief/" & j.cID, Left(z, 127), j.hColl)
                        tempitemList.Add(tempitem)
                    End If
                Next
                itemList = tempitemList
            End If

        End If
        query = totalRecords & " collections found searching " & wheretosearch & " for " & searching


        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        tempqueries = New GetQueries(tempqueries, db2.GetLoggedInformation()).rtrnList
        queries = tempqueries
        errors = temperrors
    End Sub

End Class
