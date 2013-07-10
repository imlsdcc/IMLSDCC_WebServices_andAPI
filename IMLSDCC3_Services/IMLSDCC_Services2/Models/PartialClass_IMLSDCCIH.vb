Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.IO

Partial Public Class IMLSDCCIHDataContext
    Private logBuilder As New StringBuilder()

    Public Function GetLoggedInformation() As [String]
        Return logBuilder.ToString()
    End Function

    Private Sub OnCreated()
        Log = New StringWriter(logBuilder)
    End Sub
End Class
