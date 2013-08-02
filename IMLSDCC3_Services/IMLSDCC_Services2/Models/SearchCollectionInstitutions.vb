Imports System.Web.Script.Serialization
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Data
Imports System.Linq.Expressions
Imports System.Data.Linq.SqlClient

Public Class SearchCollectionInstitutions
    Public Class collections
        Public Property uri As String
        Public Property text As String
        Public Sub New(ByVal xuri As String, ByVal xtext As String)
            uri = xuri
            text = xtext
        End Sub
    End Class


    Public Property cStartRecord As Integer

    Public Property cMaximumRecords As Integer

    Public Property totalRecords As Integer

    Public Property uriThisPage As String

    Public Property uriPreviousPage As String

    Public Property uriNextPage As String

    Public Property itemList As List(Of collections)
    Public Property errors As List(Of String)
    Public Property queries As List(Of String)


    Public Sub New(ByVal searchargs1 As String, ByVal wheretosearch As String, ByVal startRecord1 As Integer, ByVal maximumRecords1 As Integer, ByVal scope1 As String, ByVal sort1 As String)
        Dim db = New IMLSDCCIHDataContext
        Dim instlist As String()
        instlist = searchargs1.Split(",")
        Dim temperrors As New List(Of String)
        Dim tempqueries As New List(Of String)
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

        uriThisPage = "/Search/Collections?institution=" & searchargs1 & "&cStartRecord=" & startRecord1 & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
        uriNextPage = "/Search/Collections?institution=" & searchargs1 & "&cStartRecord=" & startRecord1 + maximumRecords1 & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
        uriPreviousPage = "/Search/Collections?institution=" & searchargs1 & "&cStartRecord=" & startRecord1 - maximumRecords1 & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1

        Dim ret = (From c In db.CollectionInstitutions Join col In db.Collections On c.collectionID Equals col.collectionID Where instlist.Contains(c.institutionID) And col.physical = False Select c.collectionID Distinct)

        totalRecords = ret.Count
        ret = ret.Skip(startRecord1 - 1).Take(maximumRecords1)

        Dim tempitemList As New List(Of collections)
        Dim list As New List(Of String)
        If Not ret Is Nothing Then

            For Each j In ret
                Dim rs = (From c In db.CollectionProperties Where c.collectionID = j And c.property = "title_collection" Select c.text)
                Dim z = rs.Single
                Dim tempitem As New collections("/Collection/Detail/" & j, Left(z, 127))
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
