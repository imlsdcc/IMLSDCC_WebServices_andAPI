Imports System.Xml
Imports System.Data.Linq.SqlClient

Public Class BrowseItemTypes

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
        Dim db = New IMLSDCCDataContext
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
        If (startsWith1 = "0-9") Then
            startsWith1 = "[0-9]"
        End If
        uriThisPage = "/Browse/Item/Types?startsWith=" & startsWith1 & "&startRecord=" & startRecord & "&maximumRecords=" & maximumRecords
        uriNextPage = "/Browse/Item/Types?startsWith=" & startsWith1 & "&startRecord=" & startRecord + maximumRecords & "&maximumRecords=" & maximumRecords
        uriPreviousPage = "/Browse/Item/Types?startsWith=" & startsWith1 & "&startRecord=" & startRecord - maximumRecords & "&maximumRecords=" & maximumRecords

        'Dim rs = (From r In db.Types Where r.typeNoPunct.StartsWith(startsWith1) Order By r.typeNoPunct Select r.typeText, r.typeID)
        Dim rs = (From r In db.Types Where SqlMethods.Like(r.typeNoPunct, startsWith1 & "%") Order By r.typeNoPunct Select r.typeText, r.typeID)

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
                    Dim tempitem As New items("/Search/Items?type=" & j.typeID & "&startRecord=" & startRecord & "&maximumRecords=" & maximumRecords & "&scope=&sort=", j.typeText) 'this will need to search items by subject
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
