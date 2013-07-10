Public Class SubjectBrowse
    Public Property subjectCount As List(Of subjectCount)
    Public Property queries As List(Of String)

    Public Sub New()
        Dim db As New IMLSDCCDataContext
        Dim tempqueries As New List(Of String)
        subjectCount = New List(Of subjectCount)
        Dim r = (db.GetSubjectCount).ToList

        For Each col In r
            Dim sc As New subjectCount
            sc.subjectName = col.subjectName
            sc.subjectCount = col.subjectCount

            subjectCount.Add(sc)
        Next

        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        queries = tempqueries
    End Sub
End Class



Public Class subjectCount
    Public Property subjectName As String
    Public Property subjectCount As Integer

End Class
