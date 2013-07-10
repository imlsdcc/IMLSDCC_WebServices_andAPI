Imports System.Xml

Public Class BrowseCollectionSubjects

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


    Public Sub New(ByVal id As String, ByVal startsWith1 As String, ByVal startRecord1 As Integer, ByVal maximumRecords1 As Integer)
        Dim db = New IMLSDCCIHDataContext
        Dim temperrors As New List(Of String)
        Dim tempqueries As New List(Of String)
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

        uriThisPage = "/Browse/Collection/Subjects?startsWith=" & startsWith1 & "&startRecord=" & startRecord & "&maximumRecords=" & maximumRecords
        uriNextPage = "/Browse/Collection/Subjects?startsWith=" & startsWith1 & "&startRecord=" & startRecord + maximumRecords & "&maximumRecords=" & maximumRecords
        uriPreviousPage = "/Browse/Collection/Subjects?startsWith=" & startsWith1 & "&startRecord=" & startRecord - maximumRecords & "&maximumRecords=" & maximumRecords

        If startRecord1 > 0 Then
            startRecord1 = startRecord1 - 1
        End If


        Dim rscount = (From r In db.CollectionProperties Where r.property = "subject" And r.text.StartsWith(startsWith1) Order By r.text Select r.text).Distinct
        Dim rs = (From r In db.CollectionProperties Where r.property = "subject" And r.text.StartsWith(startsWith1) Order By r.text Select r.text).Distinct.Skip(startRecord1).Take(maximumRecords1)

        Dim xcount = rscount.Count
        totalRecords = xcount
        If rscount.Count = 0 Then
            temperrors.Add("No results found.")
        Else
            Dim tempsubj As New List(Of String)
            If Not rs Is Nothing Then
                For Each k In rs
                    tempsubj.Add(k)
                Next
            End If
            tempsubj = tempsubj.Distinct().ToList()
            Dim tempitemList As New List(Of items)
            Dim list As New List(Of String)
            For Each j In tempsubj
                Dim tempitem As New items("/Search/Collection/subject=" & j & "&startRecord=1&maximumRecords=" & maximumRecords, j)
                tempitemList.Add(tempitem)
            Next
            itemList = tempitemList
        End If

        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        queries = tempqueries
        errors = temperrors
    End Sub

End Class
