Imports System.Xml

Public Class BrowseInstitutionStates

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


    Public Sub New(ByVal id As String, ByVal startsWith1 As String, ByVal startRecord1 As Integer, ByVal maximumRecords1 As Integer, ByVal state As String)
        Dim db = New IMLSDCCIHDataContext

        Dim tempqueries As New List(Of String)
        Dim temperrors As New List(Of String)
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

        uriThisPage = "/Browse/Institution/States?startsWith=" & startsWith1 & "&startRecord=" & startRecord & "&maximumRecords=" & maximumRecords
        uriNextPage = "/Browse/Institution/States?startsWith=" & startsWith1 & "&startRecord=" & startRecord + maximumRecords & "&maximumRecords=" & maximumRecords
        uriPreviousPage = "/Browse/Institution/States?startsWith=" & startsWith1 & "&startRecord=" & startRecord - maximumRecords & "&maximumRecords=" & maximumRecords

        If startRecord1 > 0 Then
            startRecord1 = startRecord1 - 1
        End If

        If state Is Nothing Then
            'Dim rscount = (From r In db.InstitutionProperties Where r.property = "state" And r.text.StartsWith(startsWith1) Order By r.text Select r.text).Distinct
            Dim rs = (From r In db.InstitutionProperties Where r.property = "state" And r.text.StartsWith(startsWith1) Order By r.text Select r.text)
            Dim xcount = rs.Count
            totalRecords = xcount
            If rs.Count = 0 Then
                temperrors.Add("No results found.")
            Else
                Dim temptype As New List(Of String)
                If Not rs Is Nothing Then
                    For Each k In rs.Distinct.Skip(startRecord1).Take(maximumRecords1)
                        temptype.Add(k)
                    Next
                End If

                Dim tempitemList As New List(Of items)
                Dim list As New List(Of String)
                For Each j In temptype
                    Dim tempitem As New items("/Browse/Institution/state=" & j & "&startRecord=1&maximumRecords=" & maximumRecords, j)
                    tempitemList.Add(tempitem)
                Next
                itemList = tempitemList
            End If
        Else
            'Dim rscount = (From r In db.InstitutionProperties Where r.property = "state" And r.text.StartsWith(startsWith1) Order By r.text Select r.text).Distinct
            Dim rs = (From r In db.InstitutionProperties Where r.property = "state" And r.text.StartsWith(state) Order By r.text Select r.institutionID)
            Dim xcount = rs.Count
            totalRecords = xcount
            If rs.Count = 0 Then
                temperrors.Add("No results found.")
            Else
                Dim temptype As New List(Of String)
                If Not rs Is Nothing Then
                    For Each k In rs.Distinct.Skip(startRecord1).Take(maximumRecords1)
                        temptype.Add(k)
                    Next
                End If

                Dim tempitemList As New List(Of items)
                Dim list As New List(Of String)
                For Each j In temptype
                    Dim iname = (From inst In db.Institutions Where inst.institutionID = j Select inst.institutionName).Take(1)
                    Dim tempitem As New items("/ResultList/Collections/1?institution=" & j & "&startRecord=1&maximumRecords=10", iname.First)
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
