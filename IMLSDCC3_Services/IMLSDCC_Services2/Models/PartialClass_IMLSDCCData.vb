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



Partial Public Class IMLSDCCDataContext
    <Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetItemsWithFacets"), ResultType(GetType(containsTableQueryResult)), ResultType(GetType(FacetsWithRecords))> _
    Public Function GetItemsWithFacets(<Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal phrase As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal queryType As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="Int")> ByVal top_n As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal dateIDs As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal typeIDs As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal placeIDs As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal collectionIDs As String) As IMultipleResults
        Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), phrase, queryType, top_n, dateIDs, typeIDs, placeIDs, collectionIDs)
        Return CType(result.ReturnValue, IMultipleResults)
    End Function
    
    <Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetCollectionsWithFacets"), ResultType(GetType(collCTQResult)), ResultType(GetType(FacetsWithRecords))> _
    Public Function GetCollectionsWithFacets(<Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal phrase As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal queryType As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="Int")> ByVal top_n As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal dateIDs As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal typeIDs As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal placeIDs As String) As IMultipleResults
        Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), phrase, queryType, top_n, dateIDs, typeIDs, placeIDs)
        Return CType(result.ReturnValue, IMultipleResults)
    End Function

    Private logBuilder As New StringBuilder()

    Public Function GetLoggedInformation() As [String]
        Return logBuilder.ToString()
    End Function

    Private Sub OnCreated()
        Log = New StringWriter(logBuilder)
    End Sub
End Class



'********Old versions*******
'Items
'<Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetItemsWithFacets"), _
'ResultType(GetType(containsTableQueryResult)), _
'ResultType(GetType(FacetsWithRecords))> _
'Public Function GetItemsWithFacetsMultiple(<Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal phrase As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal queryType As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="Int")> ByVal top_n As System.Nullable(Of Integer)) As IMultipleResults
'    Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), phrase, queryType, top_n)
'    Return CType(result.ReturnValue, IMultipleResults)
'End Function

'Collections
'<Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetCollectionsWithFacets"), ResultType(GetType(collCTQResult)), ResultType(GetType(FacetsWithRecords))> _
'Public Function GetCollectionsWithFacetsMultiple(<Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal phrase As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="NVarChar(255)")> ByVal queryType As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(DbType:="Int")> ByVal top_n As System.Nullable(Of Integer)) As IMultipleResults
'    Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), phrase, queryType, top_n)
'    Return CType(result.ReturnValue, IMultipleResults)
'End Function