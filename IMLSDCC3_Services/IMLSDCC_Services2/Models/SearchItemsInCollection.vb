Public Class SearchItemsInCollection

    Public Class items
        Public Property uri As String
        Public Property text As String
        Public Sub New(ByVal xuri As String, ByVal xtext As String)
            uri = xuri
            text = xtext
        End Sub
    End Class

    Public Property iStartRecord As Integer

    Public Property iMaximumRecords As Integer

    Public Property totalRecords As Integer

    Public Property uriThisPage As String

    Public Property uriPreviousPage As String

    Public Property uriNextPage As String

    Public Property itemList As List(Of items)

    Public Property errors As List(Of String)
    Public Property queries As List(Of String)


    Public Sub New(ByVal id As String, ByVal startRecord1 As Integer, ByVal maximumRecords1 As Integer)
        Dim db = New IMLSDCCDataContext
        Dim colllist As String()
        colllist = id.Split(",")
        Dim temperrors As New List(Of String)
        Dim tempqueries As New List(Of String)
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

        uriThisPage = "/Search/Items?" & "collections=" & id & "&iStartRecord=" & startRecord1 & "&iMaximumRecords=" & maximumRecords1
        uriNextPage = "/Search/Items?" & "collections=" & id & "&iStartRecord=" & startRecord1 + maximumRecords1 & "&iMaximumRecords=" & maximumRecords1
        uriPreviousPage = "/Search/Items?" & "&collections=" & id & "&iStartRecord=" & startRecord1 - maximumRecords1 & "&iMaximumRecords=" & maximumRecords1

        'Dim rscount = (From r In db.Records Where r.titleNoPunct.StartsWith(startsWith1) Select r.recordID)
        Dim rs = (From r In db.Records Join rtd In db.RecordsToCollections On r.recordID Equals rtd.recordID Where colllist.Contains(rtd.collID) Order By r.titleNoPunct Select r.titleText, r.recordID)

        Dim xcount = rs.Count
        totalRecords = xcount
        If rs.Count = 0 Then
            temperrors.Add("No results found.")
        Else
            rs = rs.Skip(startRecord1).Take(maximumRecords1)
            Dim tempitemList As New List(Of items)


            Dim list As New List(Of String)
            If Not rs Is Nothing Then

                For Each j In rs
                    Dim tempitem As New items("/Item/Detail/" & j.recordID, Left(j.titleText, 127))
                    tempitemList.Add(tempitem)
                Next
                itemList = tempitemList
            End If

        End If

        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        queries = tempqueries
        errors = temperrors

    End Sub
End Class
