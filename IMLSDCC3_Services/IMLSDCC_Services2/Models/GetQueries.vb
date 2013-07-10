Public Class GetQueries
    Public Property rtrnList As List(Of String)

    Public Sub New(ByVal queryList As List(Of String), ByVal dbQueryString As String)
        Dim boolRtrn = True

        If boolRtrn = True Then
            For Each i In Regex.Split(dbQueryString, "\r\n\r\n")
                If Not i = "" Then
                    queryList.Add(i.Replace(vbCrLf, " "))
                End If
            Next
            rtrnList = queryList
        Else
            rtrnList = Nothing
        End If
    End Sub
End Class
