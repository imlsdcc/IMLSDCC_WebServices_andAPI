Imports System.Xml

Public Class BrowseInstitutionTypes
    Public Class itemsResult
        Public Property text As String
        Public Sub New(ByVal rtext As String)
            text = rtext
        End Sub
    End Class
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


    Public Sub New(ByVal id As String, ByVal startsWith1 As String, ByVal startRecord1 As Integer, ByVal maximumRecords1 As Integer, ByVal type As String)
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

        uriThisPage = "/Browse/Institution/Types?startsWith=" & startsWith1 & "&startRecord=" & startRecord & "&maximumRecords=" & maximumRecords
        uriNextPage = "/Browse/Institution/Types?startsWith=" & startsWith1 & "&startRecord=" & startRecord + maximumRecords & "&maximumRecords=" & maximumRecords
        uriPreviousPage = "/Browse/Institution/Types?startsWith=" & startsWith1 & "&startRecord=" & startRecord - maximumRecords & "&maximumRecords=" & maximumRecords

        If startRecord1 > 0 Then
            startRecord1 = startRecord1 - 1
        End If

        If type Is Nothing Then
            Dim rs = (From r In db.InstitutionProperties Where r.property = "type_institution" And r.text.StartsWith(startsWith1) Order By r.text Select r.text).Distinct

            Dim xcount = 0
            If Not rs Is Nothing Then
                xcount = rs.Count()
            End If
            totalRecords = xcount
            If xcount = 0 Then
                temperrors.Add("No results found.")
            Else
                Dim tempitemList As New List(Of items)
                Dim list As New List(Of String)

                If Not rs Is Nothing Then
                    'temptype = rs.Skip(startRecord1).Take(maximumRecords1)
                    For Each k In rs.Skip(startRecord1).Take(maximumRecords1)
                        Dim tempitem As New items("/BrowseList/Institutions/type=" & k & "&startRecord=1&maximumRecords=" & maximumRecords, k)
                        tempitemList.Add(tempitem)
                    Next
                End If
                itemList = tempitemList
            End If
        Else
            ''''Get list of Institutions that have the same type as the variable "type"
            Dim rs = (From r In db.InstitutionProperties Where r.property = "type_institution" And r.text.StartsWith(type) Order By r.text Select r.institutionID).Distinct

            Dim xcount = 0
            If Not rs Is Nothing Then
                xcount = rs.Count()
            End If
            totalRecords = xcount
            If xcount = 0 Then
                temperrors.Add("No results found.")
            Else
                Dim tempitemList As New List(Of items)
                Dim list As New List(Of String)

                If Not rs Is Nothing Then
                    'temptype = rs.Skip(startRecord1).Take(maximumRecords1)
                    For Each k In rs.Skip(startRecord1).Take(maximumRecords1)
                        Dim iname = (From inst In db.Institutions Where inst.institutionID = k Select inst.institutionName).Take(1)
                        Dim tempitem As New items("/ResultList/Collections/1?institution=" & k & "&startRecord=1&maximumRecords=10", iname.First)
                        tempitemList.Add(tempitem)
                    Next
                End If
                itemList = tempitemList
            End If
        End If
        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        queries = tempqueries
        errors = temperrors
    End Sub

End Class
