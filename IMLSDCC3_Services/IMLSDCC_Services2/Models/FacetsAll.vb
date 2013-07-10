Imports System.Web.Script.Serialization
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Data
Imports System.Linq.Expressions
Imports System.Data.Linq.SqlClient


Public Class FacetsAll

    Public Class items
        Public Property uri As String
        Public Property text As String
        Public Sub New(ByVal xuri As String, ByVal xtext As String)
            uri = xuri
            text = xtext
        End Sub
    End Class


    Public Property startRecord As Integer

    Public Property maximumRecords As Integer

    Public Property totalRecords As Integer

    Public Property uriThisPage As String

    Public Property uriPreviousPage As String

    Public Property uriNextPage As String

    Public Property itemList As List(Of items)

    Public Property errors As List(Of String)
    Public Property queries As List(Of String)


    Public Sub New(ByVal searchargs1 As String(), ByVal wheretosearch As String, ByVal dates1 As String, ByVal types1 As String, ByVal places1 As String, ByVal collections1 As String, ByVal startRecord1 As Integer, ByVal maximumRecords1 As Integer, ByVal scope1 As String, ByVal sort1 As String)

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
            startRecord = startRecord1
        Else
            startRecord = 1
            startRecord1 = 1
        End If
        'startRecord = startRecord1
        If maximumRecords1 Then
            maximumRecords = maximumRecords1
        Else
            maximumRecords = 10
            maximumRecords1 = 10
        End If
        uriThisPage = "/Facets/All?" & searchq & "&dates=" & dates1 & "&types=" & types1 & "&places=" & places1 & "&collections=" & collections1 & "&startRecord=" & startRecord1 & "&maximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
        uriNextPage = "/Facets/All?" & searchq & "&dates=" & dates1 & "&types=" & types1 & "&places=" & places1 & "&collections=" & collections1 & "&startRecord=" & startRecord + maximumRecords & "&maximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
        uriPreviousPage = "/Facets/All?" & searchq & "&dates=" & dates1 & "&types=" & types1 & "&places=" & places1 & "&collections=" & collections1 & "&startRecord=" & startRecord - maximumRecords & "&maximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1

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
        'uriThisPage = myPhrase
        Dim ret = From ct In db.GetFacetsCTQ2(myPhrase, wheretosearch, maxHitLimit) Select ct

        'If dates1 <> "" Then
        'Dim datelist As String()
        'datelist = dates1.Split(",")
        'ret = (From r In ret Join rtd In db.RecordsToFacets On r.facetID Equals rtd.facetID Where datelist.Contains(rtd.facetID) Select r).Distinct
        'End If
        'If types1 <> "" Then
        '    Dim typelist As String()
        '    typelist = types1.Split(",")
        '    ret = (From r In ret Join rtd In db.RecordsToFacets On r.facetID Equals rtd.facetID Where typelist.Contains(rtd.facetID) Select r).Distinct
        'End If
        If places1 <> "" Then
            Dim placelist As String()
            placelist = places1.Split(",")
            ret = (From r In ret Join rtd In db.RecordsToFacets On r.facetID Equals rtd.facetID Where placelist.Contains(rtd.facetID) Select r).Distinct
        End If
        'If collections1 <> "" Then
        '    Dim collist As String()
        '    collist = collections1.Split(",")
        '    'ret = (From r In ret Join rtd In db.RecordsToCollections On r.facetID Equals rtd.facetID Where collist.Contains(rtd.collID) Select r).Distinct
        'End If

        'ret = From r In ret Join rtd In db.Facets On r.facetID Equals rtd.facetID Where rtd.facetType = "place" Select r

        totalRecords = ret.Count
        If ret.Count = 0 Then
            temperrors.Add("No results found.")
        Else
            ret = ret.Skip(startRecord - 1).Take(maximumRecords)
            Dim tempitemList As New List(Of items)
            Dim list As New List(Of String)
            If Not ret Is Nothing Then

                For Each j In ret
                    Dim rs = (From r In db.Facets Where r.facetID = j.facetID Select r.facetValue)
                    Dim z = rs.Single
                    Dim tempitem As New items("/Search/Items?" & searchq & "dates=&types=&places=" & j.facetID & "&collections=&startRecord=" & startRecord1 & "&maximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1, Left(z, 127))
                    tempitemList.Add(tempitem)
                Next
                itemList = tempitemList
            End If

        End If

        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        tempqueries = New GetQueries(tempqueries, db2.GetLoggedInformation()).rtrnList
        queries = tempqueries
        errors = temperrors

    End Sub

End Class
