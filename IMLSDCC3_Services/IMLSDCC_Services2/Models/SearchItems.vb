Imports System.Web.Script.Serialization
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Data
Imports System.Linq.Expressions
Imports System.Data.Linq
Imports System.Data.Linq.SqlClient


Public Class SearchItems

    Public Class items
        Public Property uri As String
        Public Property briefuri As String
        Public Property text As String
        Public Sub New(ByVal xuri As String, ByVal buri As String, ByVal xtext As String)
            uri = xuri
            briefuri = buri
            text = xtext
        End Sub
    End Class


    Public Property iStartRecord As Integer

    Public Property query As String

    Public Property iMaximumRecords As Integer

    Public Property totalRecords As Integer

    Public Property uriThisPage As String

    Public Property uriPreviousPage As String

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

        For Each args In searchargs1
            searching = searching & " " & args
            If wheretosearch = "creator" Then
                wheretosearch = "agent"
            End If
            searchq = searchq & wheretosearch & "=" & args & "&"
        Next



        Dim phrase = Trim(searching)
        myPhrase = GetItemService.Utilities.CleanForSQL(searching)
        myPhrase = Trim(myPhrase)

        If startRecord1 Then
            iStartRecord = startRecord1
        Else
            iStartRecord = 1
            startRecord1 = 1
        End If
        'startRecord = startRecord1
        If maximumRecords1 Then
            iMaximumRecords = maximumRecords1
        Else
            iMaximumRecords = 10
            maximumRecords1 = 10
        End If
        'maximumRecords = maximumRecords1
        totalRecords = 0

        uriThisPage = "/Search/Items?" & searchq & "idates=" & dates1 & "&itypes=" & types1 & "&iplaces=" & places1 & "&collections=" & collections1 & "&iStartRecord=" & startRecord1 & "&iMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
        uriNextPage = "/Search/Items?" & searchq & "idates=" & dates1 & "&itypes=" & types1 & "&iplaces=" & places1 & "&collections=" & collections1 & "&iStartRecord=" & iStartRecord + iMaximumRecords & "&iMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
        uriPreviousPage = "/Search/Items?" & searchq & "idates=" & dates1 & "&itypes=" & types1 & "&iplaces=" & places1 & "&collections=" & collections1 & "&iStartRecord=" & iStartRecord - iMaximumRecords & "&iMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1

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
        'Dim facets As String = String.Join(",", facetlist)

        'itemFacets = New List(Of FacetsWithRecords)  Dim ret As List(Of containsTableQueryResult)
        Dim ret As List(Of containsTableQueryResult), retFacets As List(Of FacetsWithRecords.facets), retTemp As IMultipleResults, retItems
        If withFacets = True Then
            retTemp = db.GetItemsWithFacets(myPhrase, wheretosearch, maxHitLimit, dates1, types1, places1, collections1)
            retItems = retTemp.GetResult(Of containsTableQueryResult)().ToList()
            retFacets = retTemp.GetResult(Of FacetsWithRecords.facets)().ToList()
            itemFacets = retFacets.ToList()
        Else
            'retTemp = db.GetItemsWithFacets(myPhrase, wheretosearch, maxHitLimit, dates1, types1, places1, collections1)
            'retItems = retTemp.GetResult(Of containsTableQueryResult)().ToList()
            retItems = (From ct In db.containsTableQuery(myPhrase, wheretosearch, maxHitLimit) Order By ct.ctRank Descending Select ct).ToList
        End If

        If searching = " " Then
            retItems = (From r In db.RecordsToCollections Select r).ToList
        End If

        ret = retItems
        'If dates1 <> "" Then
        '    Dim datelist As String()
        '    datelist = dates1.Split(",")
        '    ret = (From r In ret Join rtd In db.RecordsToFacets On r.RecordID Equals rtd.recordID Where datelist.Contains(rtd.facetID) Order By r.ctRank Descending Select r).Distinct.ToList()
        'End If
        'If types1 <> "" Then
        '    Dim typelist As String()
        '    typelist = types1.Split(",")
        '    ret = (From r In ret Join rtd In db.RecordsToFacets On r.RecordID Equals rtd.recordID Where typelist.Contains(rtd.facetID) Order By r.ctRank Descending Select r).Distinct.ToList()
        'End If
        'If places1 <> "" Then
        '    Dim placelist As String()
        '    placelist = places1.Split(",")
        '    ret = (From r In ret Join rtd In db.RecordsToFacets On r.RecordID Equals rtd.recordID Where placelist.Contains(rtd.facetID) Order By r.ctRank Descending Select r).Distinct.ToList()
        'End If
        'If collections1 <> "" Then
        '    Dim collist As String()
        '    collist = collections1.Split(",")
        '    ret = (From r In ret Join rtd In db.RecordsToCollections On r.RecordID Equals rtd.recordID Where collist.Contains(rtd.collID) Order By r.ctRank Descending Select r).Distinct.ToList()
        'End If

        totalRecords = ret.Count
        If ret.Count = 0 Then
            temperrors.Add("No results found.")
        Else
            ret = ret.Skip(iStartRecord - 1).Take(iMaximumRecords).ToList()
            Dim tempitemList As New List(Of items)
            Dim list As New List(Of String)
            If Not ret Is Nothing Then

                For Each j In ret
                    Dim rs = (From r In db.Records Where r.recordID = j.RecordID Select r.shortXML)
                    Dim z = rs.Single
                    Dim dom As New XmlDocument
                    Dim title As String
                    dom.LoadXml(z.ToString)
                    If Not (dom.SelectSingleNode("//property[@name='title']/value") Is Nothing) Then
                        title = dom.SelectSingleNode("//property[@name='title']/value").InnerText
                    End If


                    Dim tempitem As New items("/Item/Detail/" & j.RecordID, "/Item/Brief/" & j.RecordID, Left(title, 127))
                    tempitemList.Add(tempitem)
                Next
                itemList = tempitemList
            End If
        End If

        query = totalRecords & " records found searching " & wheretosearch & " for " & searching

        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        tempqueries = New GetQueries(tempqueries, db2.GetLoggedInformation()).rtrnList
        queries = tempqueries
        errors = temperrors
    End Sub

End Class