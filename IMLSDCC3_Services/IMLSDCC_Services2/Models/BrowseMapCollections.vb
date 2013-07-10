Public Class BrowseMapCollections
    Public Property statesCount As List(Of StatesCount)
    Public Property queries As List(Of String)

    Public Sub New()
        Dim db As New IMLSDCCDataContext
        Dim tempqueries As New List(Of String)

        statesCount = New List(Of StatesCount)
        Dim r = (db.GetStateCount).ToList

        For Each col In r
            Dim sc As New StatesCount
            sc.stateCount = col.stateCount
            sc.stateName = col.stateName
            sc.stateID = col.stateID

            statesCount.Add(sc)
        Next

        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        queries = tempqueries
    End Sub
End Class



Public Class StatesCount
    Public Property stateID As Integer
    Public Property stateName As String
    Public Property stateCount As Integer

End Class
