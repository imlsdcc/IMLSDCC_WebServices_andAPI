Public Class BrowseStats
    Public Property itemCount As Integer

    Public Property collectionCount As Integer

    Public Property institutionCount As Integer

    Public Property queries As List(Of String)

    Public Sub New()
        Dim db As New IMLSDCCDataContext
        Dim tempqueries As New List(Of String)

        Dim r = (db.GetStats()).ToList

        itemCount = r(0).ItemCount
        collectionCount = r(0).CollectionCount
        institutionCount = r(0).InstitutionCount

        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        queries = tempqueries
    End Sub
End Class
