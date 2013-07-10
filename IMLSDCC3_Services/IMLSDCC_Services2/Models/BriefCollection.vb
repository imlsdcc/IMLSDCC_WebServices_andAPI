Imports System.Xml
Imports System.Data.Linq

Public Class BriefCollection

    Public Property title As String

    Public Property description As String
    Public Property detailurl As String
    Public Property uniqueid As String
    Public Property isavailableat As String
    Public Property thumbnail1 As String
    Public Property thumbnail2 As String
    Public Property thumbnail3 As String
    Public Property thumbnail4 As String
    Public Property errors As List(Of String)
    Public Property queries As List(Of String)

    Public Sub New(ByVal id As Integer)

        Dim db = New IMLSDCCIHDataContext
        Dim db2 = New IMLSDCCDataContext
        Dim temperrors As New List(Of String)
        Dim tempqueries As New List(Of String)

        detailurl = "/collection/detail/" & id
        uniqueid = "/collection/brief/" & id

        Dim rs = From r In db.ShortDisplays Where r.collectionID = id Select r.title_collection, r.description, r.isAvailableAt_URL


        If rs.Count() > 0 Then
            Dim z = rs.Single

            Dim rsthumb = From s In db2.CollectionsToThumbnails Where s.collID = id Select s.oaiidentifier1, s.oaiidentifier2, s.oaiidentifier3, s.oaiidentifier4
            'queries.Add(sw.ToString)
            If rsthumb.Count() > 0 Then
                Dim y = rsthumb.Single
                thumbnail1 = "http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=" & y.oaiidentifier1
                thumbnail2 = "http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=" & y.oaiidentifier2
                thumbnail3 = "http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=" & y.oaiidentifier3
                thumbnail4 = "http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=" & y.oaiidentifier4
            End If

            title = z.title_collection
            description = z.description
            isavailableat = z.isAvailableAt_URL
            tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
            tempqueries = New GetQueries(tempqueries, db2.GetLoggedInformation()).rtrnList
            queries = tempqueries
        Else
            temperrors.Add("Record does not exist.")
            tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
            queries = tempqueries
        End If
        errors = temperrors
    End Sub
End Class