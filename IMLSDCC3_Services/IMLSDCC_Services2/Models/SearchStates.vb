Public Class SearchStates
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

    Public Property iMaximumRecords As Integer

    Public Property totalRecords As Integer

    Public Property uriThisPage As String

    Public Property uriPreviousPage As String

    Public Property uriNextPage As String

    Public Property itemList As List(Of items)
    Public Property errors As List(Of String)
    Public Property queries As List(Of String)

    Public Sub New(ByVal searchargs1 As String, ByVal wheretosearch As String, ByVal startRecord1 As Integer, ByVal maximumRecords1 As Integer, ByVal scope1 As String, ByVal sort1 As String)
        Dim db = New IMLSDCCDataContext
        Dim db2 = New IMLSDCCIHDataContext
        Dim typelist As String()
        typelist = searchargs1.Split(",")
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

        uriThisPage = "/Search/Items?state=" & searchargs1 & "&iStartRecord=" & startRecord1 & "&iMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
        uriNextPage = "/Search/Items?state=" & searchargs1 & "&iStartRecord=" & startRecord1 + maximumRecords1 & "&iMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
        uriPreviousPage = "/Search/Items?state=" & searchargs1 & "&iStartRecord=" & startRecord1 - maximumRecords1 & "&iMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1

        Dim ret = (From r In db.RecordsToFacets Where typelist.Contains(r.facetID) Select r.recordID).Distinct

        totalRecords = ret.Count
        ret = ret.Skip(startRecord1 - 1).Take(maximumRecords1)

        Dim tempitemList As New List(Of items)
        Dim list As New List(Of String)
        If Not ret Is Nothing Then

            For Each j In ret
                Dim rs = (From r In db.Records Where r.recordID = j Select r.title)
                Dim z = rs.Single
                Dim tempitem As New items("/Item/Detail/" & j, "/Item/Brief/" & j, Left(z, 127))
                tempitemList.Add(tempitem)
            Next
            itemList = tempitemList
        Else
            temperrors.Add("No results found.")
        End If

        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        queries = tempqueries
        errors = temperrors
    End Sub
End Class
